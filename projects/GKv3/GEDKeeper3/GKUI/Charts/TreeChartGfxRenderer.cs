﻿/*
 *  "GEDKeeper", the personal genealogical database editor.
 *  Copyright (C) 2009-2017 by Sergey V. Zhdanovskih.
 *
 *  This file is part of "GEDKeeper".
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Text;
using BSLib;
using Eto.Drawing;
using GKCore.Charts;
using GKCore.Interfaces;
using GKUI.Components;

namespace GKUI.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TreeChartGfxRenderer : ChartRenderer
    {
        private Graphics fCanvas;
        private float fTranslucent;

        private TextWriter fSVGWriter;
        private SvgGraphics fSVGGfx;

        public override bool IsSVG { get { return (fSVGWriter != null); } }

        public TreeChartGfxRenderer() : base()
        {
        }

        public override void SetSVGMode(bool active, string svgFileName, int width, int height)
        {
            if (active) {
                fSVGWriter = new StreamWriter(new FileStream(svgFileName, FileMode.Create), Encoding.UTF8);
                fSVGGfx = new SvgGraphics(fSVGWriter, ExtRectF.CreateBounds(0, 0, width, height));
                fSVGGfx.BeginDrawing();
            } else {
                if (fSVGWriter != null) {
                    fSVGGfx.EndDrawing();
                    fSVGGfx = null;

                    fSVGWriter.Flush();
                    fSVGWriter.Close();
                    fSVGWriter = null;
                }
            }
        }

        public override void SetTarget(object target)
        {
            Graphics gfx = target as Graphics;
            if (gfx == null)
                throw new ArgumentException(@"Argument's type mismatch", "target");

            fCanvas = gfx;
            fCanvas.AntiAlias = true;
        }

        public override void DrawImage(IImage image, float x, float y,
                                       float width, float height)
        {
            var sdImage = ((ImageHandler)image).Handle;

            fCanvas.DrawImage(sdImage, x, y, width, height);
        }

        public override void DrawImage(IImage image, ExtRect destinationRect,
            ExtRect sourceRect)
        {
            var sdImage = ((ImageHandler)image).Handle;

            Rectangle destRect = UIHelper.Rt2Rt(destinationRect);
            Rectangle sourRect = UIHelper.Rt2Rt(sourceRect);
            fCanvas.DrawImage(sdImage, sourRect, destRect);
        }

        public override int GetTextHeight(IFont font)
        {
            Font sdFnt = ((FontHandler)font).Handle;

            return (int)sdFnt.MeasureString(STR_HEIGHT_SAMPLE).Height;
        }

        public override int GetTextWidth(string text, IFont font)
        {
            Font sdFnt = ((FontHandler)font).Handle;

            return (int)sdFnt.MeasureString(text).Width;
        }

        public override ExtSizeF GetTextSize(string text, IFont font)
        {
            return new ExtSizeF(GetTextWidth(text, font), GetTextHeight(font));
        }

        public override void DrawString(string text, IFont font, IBrush brush, float x, float y)
        {
            SolidBrush sdBrush = (SolidBrush)((BrushHandler)brush).Handle;
            Font sdFnt = ((FontHandler)font).Handle;

            fCanvas.DrawText(sdFnt, sdBrush, x, y, text);

            if (fSVGGfx != null) {
                fSVGGfx.SetFont(font);
                fSVGGfx.SetColor(brush.Color);
                fSVGGfx.DrawString(text, x, y);
            }
        }

        public override void DrawLine(IPen pen, float x1, float y1, float x2, float y2)
        {
            Pen sdPen = ((PenHandler)pen).Handle;

            fCanvas.DrawLine(sdPen, x1, y1, x2, y2);

            if (fSVGGfx != null) {
                fSVGGfx.SetColor(pen.Color);
                fSVGGfx.DrawLine(x1, y1, x2, y2, pen.Width);
            }
        }

        public override void DrawRectangle(IPen pen, IColor fillColor,
                                           float x, float y, float width, float height)
        {
            Color sdFillColor = ((ColorHandler)fillColor).Handle;

            using (GraphicsPath path = CreateRectangle(x, y, width, height)) {
                if (sdFillColor != Colors.Transparent) {
                    sdFillColor = PrepareColor(sdFillColor);

                    fCanvas.FillPath(new SolidBrush(sdFillColor), path);
                }

                if (pen != null) {
                    Pen sdPen = ((PenHandler)pen).Handle;
                    fCanvas.DrawPath(sdPen, path);
                }
            }

            if (fSVGGfx != null) {
                if (sdFillColor != Colors.Transparent) {
                    fSVGGfx.SetColor(fillColor);
                    fSVGGfx.FillRect(x, y, width, height);
                }

                if (pen != null) {
                    fSVGGfx.SetColor(pen.Color);
                    fSVGGfx.DrawRect(x, y, width, height, pen.Width);
                }
            }
        }

        public override void DrawRoundedRectangle(IPen pen, IColor fillColor, float x, float y,
                                                  float width, float height, float radius)
        {
            Color sdFillColor = ((ColorHandler)fillColor).Handle;

            using (GraphicsPath path = CreateRoundedRectangle(x, y, width, height, radius)) {
                if (sdFillColor != Colors.Transparent) {
                    sdFillColor = PrepareColor(sdFillColor);

                    fCanvas.FillPath(new SolidBrush(sdFillColor), path);
                }

                if (pen != null) {
                    Pen sdPen = ((PenHandler)pen).Handle;
                    fCanvas.DrawPath(sdPen, path);
                }
            }

            if (fSVGGfx != null) {
                if (sdFillColor != Colors.Transparent) {
                    fSVGGfx.SetColor(fillColor);
                    fSVGGfx.FillRoundedRect(x, y, width, height, radius);
                }

                if (pen != null) {
                    fSVGGfx.SetColor(pen.Color);
                    fSVGGfx.DrawRoundedRect(x, y, width, height, radius, pen.Width);
                }
            }
        }

        public override void FillPath(IBrush brush, IGfxPath path)
        {
            Brush sdBrush = ((BrushHandler)brush).Handle;
            GraphicsPath sdPath = ((GfxPathHandler)path).Handle;
            fCanvas.FillPath(sdBrush, sdPath);

            if (fSVGGfx != null) {
                if (path is IGfxCirclePath) {
                    var circlePath = path as IGfxCirclePath;
                    fSVGGfx.DrawEllipse(circlePath.X, circlePath.Y, circlePath.Width, circlePath.Height, null /*pen*/, brush);
                } else if (path is IGfxCircleSegmentPath) {
                    var segmPath = path as IGfxCircleSegmentPath;
                    fSVGGfx.DrawCircleSegment(0, 0, segmPath.InRad, segmPath.ExtRad, segmPath.Ang1, segmPath.Ang2, null /*pen*/, brush);
                } else {

                }
            }
        }

        public override void DrawPath(IPen pen, IGfxPath path)
        {
            Pen sdPen = ((PenHandler)pen).Handle;
            GraphicsPath sdPath = ((GfxPathHandler)path).Handle;
            fCanvas.DrawPath(sdPen, sdPath);

            if (fSVGGfx != null) {
                if (path is IGfxCirclePath) {
                    var circlePath = path as IGfxCirclePath;
                    fSVGGfx.DrawEllipse(circlePath.X, circlePath.Y, circlePath.Width, circlePath.Height, pen, null /*brush*/);
                } else if (path is IGfxCircleSegmentPath) {
                    var segmPath = path as IGfxCircleSegmentPath;
                    fSVGGfx.DrawCircleSegment(0, 0, segmPath.InRad, segmPath.ExtRad, segmPath.Ang1, segmPath.Ang2, pen, null /*brush*/);
                } else {

                }
            }
        }

        public override void DrawPath(IPen pen, IBrush brush, IGfxPath path)
        {
            GraphicsPath sdPath = ((GfxPathHandler)path).Handle;

            if (brush != null) {
                Brush sdBrush = ((BrushHandler)brush).Handle;
                fCanvas.FillPath(sdBrush, sdPath);
            }

            if (pen != null) {
                Pen sdPen = ((PenHandler)pen).Handle;
                fCanvas.DrawPath(sdPen, sdPath);
            }

            if (fSVGGfx != null) {
                if (path is IGfxCirclePath) {
                    var circlePath = path as IGfxCirclePath;
                    fSVGGfx.DrawEllipse(circlePath.X, circlePath.Y, circlePath.Width, circlePath.Height, pen, brush);
                } else if (path is IGfxCircleSegmentPath) {
                    var segmPath = path as IGfxCircleSegmentPath;
                    fSVGGfx.DrawCircleSegment(0, 0, segmPath.InRad, segmPath.ExtRad, segmPath.Ang1, segmPath.Ang2, pen, brush);
                } else {

                }
            }
        }

        public override void DrawCircle(IPen pen, IBrush brush, float x, float y,
                                        float width, float height)
        {
            if (fSVGGfx != null) {
                fSVGGfx.DrawEllipse(x, y, width, height, pen, brush);
            }
        }

        public override void DrawCircleSegment(IPen pen, IBrush brush, int ctX, int ctY,
                                               float inRad, float extRad,
                                               float startAngle, float wedgeAngle)
        {
            if (fSVGGfx != null) {
                fSVGGfx.DrawCircleSegment(ctX, ctY, inRad, extRad,
                                          startAngle, startAngle + wedgeAngle,
                                          pen, brush);
            }
        }

        private Color PrepareColor(Color color)
        {
            float alpha = (1 - fTranslucent);
            return new Color(color, alpha);
        }

        public override IPen CreatePen(IColor color, float width)
        {
            Color sdColor = ((ColorHandler)color).Handle;
            sdColor = PrepareColor(sdColor);

            return new PenHandler(new Pen(sdColor, width));
        }

        public override IBrush CreateSolidBrush(IColor color)
        {
            Color sdColor = ((ColorHandler)color).Handle;
            sdColor = PrepareColor(sdColor);

            return new BrushHandler(new SolidBrush(sdColor));
        }

        public override IGfxPath CreatePath()
        {
            return new GfxPathHandler(new GraphicsPath());
        }

        public override void SetTranslucent(float value)
        {
            fTranslucent = Algorithms.CheckBounds(value, 0.0f, 1.0f);
        }

        #region Private helpers

        private static GraphicsPath CreateRectangle(float x, float y, float width, float height)
        {
            float xw = x + width;
            float yh = y + height;

            GraphicsPath p = new GraphicsPath();
            p.StartFigure();

            p.AddLine(x, y, xw, y); // Top Edge
            p.AddLine(xw, y, xw, yh); // Right Edge
            p.AddLine(xw, yh, x, yh); // Bottom Edge
            p.AddLine(x, yh, x, y); // Left Edge

            p.CloseFigure();
            return p;
        }

        private static GraphicsPath CreateRoundedRectangle(float x, float y, float width, float height, float radius)
        {
            float xw = x + width;
            float yh = y + height;
            float xwr = xw - radius;
            float yhr = yh - radius;
            float xr = x + radius;
            float yr = y + radius;
            float r2 = radius * 2;
            float xwr2 = xw - r2;
            float yhr2 = yh - r2;

            GraphicsPath p = new GraphicsPath();
            p.StartFigure();

            p.AddArc(x, y, r2, r2, 180, 90); // Top Left Corner
            p.AddLine(xr, y, xwr, y); // Top Edge
            p.AddArc(xwr2, y, r2, r2, 270, 90); // Top Right Corner
            p.AddLine(xw, yr, xw, yhr); // Right Edge
            p.AddArc(xwr2, yhr2, r2, r2, 0, 90); // Bottom Right Corner
            p.AddLine(xwr, yh, xr, yh); // Bottom Edge
            p.AddArc(x, yhr2, r2, r2, 90, 90); // Bottom Left Corner
            p.AddLine(x, yhr, x, yr); // Left Edge

            p.CloseFigure();
            return p;
        }

        #endregion

        public override void ScaleTransform(float sx, float sy)
        {
            fCanvas.ScaleTransform(sx, sy);

            if (fSVGGfx != null) {
                fSVGGfx.Scale(sx, sy);
            }
        }

        public override void TranslateTransform(float dx, float dy)
        {
            fCanvas.TranslateTransform(dx, dy);

            if (fSVGGfx != null) {
                fSVGGfx.Translate(dx, dy);
            }
        }

        public override void RotateTransform(float angle)
        {
            fCanvas.RotateTransform(angle);

            if (fSVGGfx != null) {
                fSVGGfx.Rotate(angle);
            }
        }

        public override void ResetTransform()
        {
            // unsupported in Eto

            if (fSVGGfx != null) {
                fSVGGfx.ResetState();
            }
        }

        public override void RestoreTransform()
        {
            fCanvas.RestoreTransform();

            if (fSVGGfx != null) {
                fSVGGfx.RestoreState();
            }
        }

        public override void SaveTransform()
        {
            fCanvas.SaveTransform();

            if (fSVGGfx != null) {
                fSVGGfx.SaveState();
            }
        }
    }
}
