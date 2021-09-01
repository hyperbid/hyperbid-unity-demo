using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperBid.Api{
    public class HBNativeItemProperty {
        public int x;
        public int y;
        public int width;
        public int height;
        public bool usesPixel;

        public string backgroundColor;
		public string textColor; //只是针对text的view有效
        public int textSize; //只是针对text的view有效
        public bool isCustomClick; //只针对Android

        public HBNativeItemProperty(int x, int y, int width, int height, string backgroundColor, string textColor, int textSize, bool usesPixel, bool isCustomClick)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.usesPixel = usesPixel;
            this.backgroundColor = backgroundColor;
            this.textColor = textColor;
            this.textSize = textSize;
            this.isCustomClick = isCustomClick;
        }


        public HBNativeItemProperty(int x, int y, int width, int height, string backgroundColor, string textColor, int textSize, bool usesPixel)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.usesPixel = usesPixel;
            this.backgroundColor = backgroundColor;
            this.textColor = textColor;
            this.textSize = textSize;
        }

        public HBNativeItemProperty(int x,int y,int width,int height,string backgroundColor,string textColor,int textSize){
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
            this.usesPixel = false;
			this.backgroundColor = backgroundColor;
			this.textColor = textColor;
			this.textSize = textSize;
		}			
    }
}
