﻿using System;

namespace UnityEngine.U2D
{
    internal interface IPixelPerfectCamera
    {
        int assetsPPU { get; set; }
        int refResolutionX { get; set; }
        int refResolutionY { get; set; }
        bool upscaleRT { get; set; }
        bool pixelSnapping { get; set; }
        bool cropFrameX { get; set; }
        bool cropFrameY { get; set; }
        bool stretchFill { get; set; }
    }

    internal class PixelPerfectCameraInternal
    {
        private IPixelPerfectCamera m_Component;

        internal float originalOrthoSize;
        internal bool hasPostProcessLayer;
        internal bool cropFrameXAndY = false;
        internal bool cropFrameXOrY = false;
        internal bool useStretchFill = false;
        internal int zoom = 1;
        internal bool useOffscreenRT = false;
        internal int offscreenRTWidth = 0;
        internal int offscreenRTHeight = 0;
        internal Rect pixelRect = Rect.zero;
        internal float orthoSize = 1.0f;
        internal float unitsPerPixel = 0.0f;

        internal PixelPerfectCameraInternal(IPixelPerfectCamera component)
        {
            m_Component = component;
        }

        internal void CalculateCameraProperties(int screenWidth, int screenHeight)
        {
            int assetsPPU = m_Component.assetsPPU;
            int refResolutionX = m_Component.refResolutionX;
            int refResolutionY = m_Component.refResolutionY;
            bool upscaleRT = m_Component.upscaleRT;
            bool pixelSnapping = m_Component.pixelSnapping;
            bool cropFrameX = m_Component.cropFrameX;
            bool cropFrameY = m_Component.cropFrameY;
            bool stretchFill = m_Component.stretchFill;

            cropFrameXAndY = cropFrameY && cropFrameX;
            cropFrameXOrY = cropFrameY || cropFrameX;
            useStretchFill = cropFrameXAndY && stretchFill;

            // zoom level (PPU scale)
            int verticalZoom = screenHeight / refResolutionY;
            int horizontalZoom = screenWidth / refResolutionX;
            zoom = Math.Max(1, Math.Min(verticalZoom, horizontalZoom));

            // off-screen RT
            useOffscreenRT = false;
            offscreenRTWidth = 0;
            offscreenRTHeight = 0;

            if (cropFrameXOrY)
            {
                if (!upscaleRT)
                {
                    if (useStretchFill)
                    {
                        useOffscreenRT = true;
                        offscreenRTWidth = zoom * refResolutionX;
                        offscreenRTHeight = zoom * refResolutionY;
                    }
                }
                else
                {
                    useOffscreenRT = true;
                    if (cropFrameXAndY)
                    {
                        offscreenRTWidth = refResolutionX;
                        offscreenRTHeight = refResolutionY;
                    }
                    else if (cropFrameY)
                    {
                        offscreenRTWidth = screenWidth / zoom / 2 * 2;   // Make sure it's an even number by / 2 * 2.
                        offscreenRTHeight = refResolutionY;
                    }
                    else    // crop frame X
                    {
                        offscreenRTWidth = refResolutionX;
                        offscreenRTHeight = screenHeight / zoom / 2 * 2;   // Make sure it's an even number by / 2 * 2.
                    }
                }
            }
            else if (upscaleRT && zoom > 1)
            {
                useOffscreenRT = true;
                offscreenRTWidth = screenWidth / zoom / 2 * 2;        // Make sure it's an even number by / 2 * 2.
                offscreenRTHeight = screenHeight / zoom / 2 * 2;
            }

            // viewport
            pixelRect = Rect.zero;

            if (cropFrameXOrY && !upscaleRT && !useStretchFill)
            {
                if (cropFrameXAndY)
                {
                    pixelRect.width = zoom * refResolutionX;
                    pixelRect.height = zoom * refResolutionY;
                }
                else if (cropFrameY)
                {
                    pixelRect.width = screenWidth;
                    pixelRect.height = zoom * refResolutionY;
                }
                else // crop frame X
                {
                    pixelRect.width = zoom * refResolutionX;
                    pixelRect.height = screenHeight;
                }

                pixelRect.x = (screenWidth - (int)pixelRect.width) / 2;
                pixelRect.y = (screenHeight - (int)pixelRect.height) / 2;
            }
            else if (useOffscreenRT)
            {
                // When Camera.forceIntoRenderTexture is true, the size of the internal RT is determined by VP size.
                // That's why we set the VP size to be (m_OffscreenRTWidth, m_OffscreenRTHeight) here.
                pixelRect = new Rect(0.0f, 0.0f, offscreenRTWidth, offscreenRTHeight);
            }

            // orthographic size
            if (cropFrameY)
                orthoSize = (refResolutionY * 0.5f) / assetsPPU;
            else if (cropFrameX)
            {
                float aspect = (pixelRect == Rect.zero) ? (float)screenWidth / screenHeight : pixelRect.width / pixelRect.height;
                orthoSize = ((refResolutionX / aspect) * 0.5f) / assetsPPU;
            }
            else if (upscaleRT && zoom > 1)
                orthoSize = (offscreenRTHeight * 0.5f) / assetsPPU;
            else
            {
                float pixelHeight = (pixelRect == Rect.zero) ? screenHeight : pixelRect.height;
                orthoSize = (pixelHeight * 0.5f) / (zoom * assetsPPU);
            }

            // Camera pixel grid spacing
            if (upscaleRT || (!upscaleRT && pixelSnapping))
                unitsPerPixel = 1.0f / assetsPPU;
            else
                unitsPerPixel = 1.0f / (zoom * assetsPPU);
        }

        internal Rect CalculatePostRenderPixelRect(float cameraAspect, int screenWidth, int screenHeight)
        {
            // This VP is used when the internal temp RT is blitted back to screen.
            Rect pixelRect = new Rect();

            if (useStretchFill)
            {
                // stretch (fit either width or height)
                float screenAspect = (float)screenWidth / screenHeight;
                if (screenAspect > cameraAspect)
                {
                    pixelRect.height = screenHeight;
                    pixelRect.width = screenHeight * cameraAspect;
                    pixelRect.x = (screenWidth - (int)pixelRect.width) / 2;
                    pixelRect.y = 0;
                }
                else
                {
                    pixelRect.width = screenWidth;
                    pixelRect.height = screenWidth / cameraAspect;
                    pixelRect.y = (screenHeight - (int)pixelRect.height) / 2;
                    pixelRect.x = 0;
                }
            }
            else
            {
                // center
                pixelRect.height = zoom * offscreenRTHeight;
                pixelRect.width = zoom * offscreenRTWidth;
                pixelRect.x = (screenWidth - (int)pixelRect.width) / 2;
                pixelRect.y = (screenHeight - (int)pixelRect.height) / 2;
            }

            return pixelRect;
        }
    }
}
