using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ktx_uint8_t = System.Byte;
using ktx_uint16_t = System.UInt16;
using ktx_int16_t = System.Int16;
using ktx_uint32_t = System.UInt32;
using ktx_int32_t = System.Int32;
using ktx_uint64_t = System.UInt64;
using ktx_int64_t = System.Int64;
using ktx_size_t = System.IntPtr;
using ktx_off_t = System.Int64;

using GLboolean = System.Byte;
using GLenum = System.UInt32;
using GLint = System.Int32;
using GLsizei = System.Int32;
using GLuint = System.UInt32;
using GLubyte = System.Byte;

using static CSharpKtxSoftwareBindings.KTXBindingsTypes;

namespace CSharpKtxSoftwareBindings
{
    public class KTXBindingsFunctions
    {
        // path to your dll or so
        // /!\ not tested on linux
        const string KtxDll = "C:\\TestInterop\\KtxSoftCSharpBindingExample\\CSharpKtxSoftwareBindings\\ktx.dll";

        [DllImport(KtxDll, CallingConvention = CallingConvention.StdCall)]
        public static extern ktx_error_code_e ktxTexture2_Create(
             ktxTextureCreateInfo createInfo,
            ktxTextureCreateStorageEnum storageAllocation,
             IntPtr newTex);

        [DllImport(KtxDll, CallingConvention = CallingConvention.StdCall)]
        public static extern ktx_error_code_e ktxTexture2_CreateCopy(
            IntPtr orig,
             IntPtr newTex);

        [DllImport(KtxDll, CallingConvention = CallingConvention.StdCall)]
        public static extern ktx_error_code_e ktxTexture2_CreateFromNamedFile(
             IntPtr filename,
            ktx_uint32_t createFlags,
             IntPtr newTex
            );

        [DllImport(KtxDll, CallingConvention = CallingConvention.StdCall)]
        public static extern ktx_error_code_e ktxTexture2_CreateFromMemory(
             ktx_uint8_t[] bytes, ktx_size_t size,
                            ktx_uint32_t createFlags,
                            IntPtr newTex);

        [DllImport(KtxDll, CallingConvention = CallingConvention.StdCall)]
        public static extern ktx_error_code_e ktxTexture2_CreateFromStream(KtxStream stream,
                             ktx_uint32_t createFlags,
                              IntPtr newTex);

        /* Compression quality. Range is [1,255].  Lower gives better
           compression/lower quality/faster. Higher gives less compression
           /higher quality/slower.  
      */
        [DllImport(KtxDll, CallingConvention = CallingConvention.StdCall)]
        public static extern ktx_error_code_e ktxTexture2_CompressBasis(IntPtr This, ktx_uint32_t quality);


        [DllImport(KtxDll, CallingConvention = CallingConvention.StdCall)]
        public static extern ktx_error_code_e ktxTexture2_CompressBasisEx(IntPtr This, IntPtr ktxparams);
    }


}
