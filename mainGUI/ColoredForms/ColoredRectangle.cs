﻿using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace ColoredForms
{
    public class ColoredRectangle : ColoredForm
    {
        public SKPoint Start { get; set; }
        public SKPoint End { get; set; }
        public float StrokeWidth { get; set; }
    }
}
