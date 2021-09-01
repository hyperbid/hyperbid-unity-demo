using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperBid.Api
{
    public class HBRect
    {
        public HBRect(int x, int y, int width, int height, bool usesPixel)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.usesPixel = usesPixel;
        }

        public HBRect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.usesPixel = false;
        }

        public int x = 0;
        public int y = 0;
        public int width = 0;
        public int height = 0;
        public bool usesPixel = false;

    }

    public class HBSize
    {
        public HBSize(int width, int height, bool usesPixel)
        {
            this.width = width;
            this.height = height;
            this.usesPixel = usesPixel;
        }

        public HBSize(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.usesPixel = false;
        }

        public int width = 0;
        public int height = 0;
        public bool usesPixel = false;
    }
}
