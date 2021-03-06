using System;

namespace OpenTK.Windowing.Common
{
    /// <summary>
    /// Defines the interface for OpenGL context management.
    /// </summary>
    public interface IGraphicsContext
    {
        /// <summary>
        /// Gets a value indicating whether the context is the current active on of the current thread.
        /// </summary>
        bool IsCurrent { get; }

        /// <summary>
        /// Gets a handle or pointer that represents the opengl context in the API used to create it.
        /// </summary>
        IntPtr NativeContex { get; }

        /// <summary>
        /// Swaps the front and back buffers of the current GraphicsContext, presenting the rendered scene to the user.
        /// </summary>
        void SwapBuffers();

        /// <summary>
        /// Makes the GraphicsContext current on the calling thread.
        /// </summary>
        void MakeCurrent();

        /// <summary>
        /// Makes no GraphicsContext current one on the calling thread.
        /// </summary>
        void MakeNoneCurrent();
    }
}
