using System.Runtime.InteropServices;
using System;

using ktx_uint8_t = System.Byte;
using ktx_uint16_t = System.UInt16 ;
using ktx_int16_t = System.Int16 ;
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
using System.IO;
using static CSharpKtxSoftwareBindings.KtxTexture;
using static CSharpKtxSoftwareBindings.ktxTexture2;
using System.Reflection.Emit;

namespace CSharpKtxSoftwareBindings
{
    public class KTXBindingsTypes
    {

        public enum ktx_bool_t : byte
        {
            KTX_FALSE = 0,
            KTX_TRUE = 1
        }

        public const string KTX_ANIMDATA_KEY = "KTXanimData";
        public const string KTX_ORIENTATION_KEY = "KTXorientation";
        public const string KTX_SWIZZLE_KEY = "KTXswizzle";
        public const string KTX_WRITER_KEY = "KTXwriter";
        public const string KTX_WRITER_SCPARAMS_KEY = "KTXwriterScParams";
        public const string KTX_ORIENTATION1_FMT = "S=%c";
        public const string KTX_ORIENTATION2_FMT = "S=%c,T=%c";
        public const string KTX_ORIENTATION3_FMT = "S=%c,T=%c,R=%c";
        public const int KTX_GL_UNPACK_ALIGNMENT = 4;
        public const int KTX_FACESLICE_WHOLE_LEVEL = KTXBindingsTypes.UINT_MAX;
        public const bool KTX_TRUE = true;
        public const bool KTX_FALSE = false;
        public const int UINT_MAX = -1;

        public enum ktx_error_code_e
        {
            KTX_SUCCESS = 0,
            KTX_FILE_DATA_ERROR,
            KTX_FILE_ISPIPE,
            KTX_FILE_OPEN_FAILED,
            KTX_FILE_OVERFLOW,
            KTX_FILE_READ_ERROR,
            KTX_FILE_SEEK_ERROR,
            KTX_FILE_UNEXPECTED_EOF,
            KTX_FILE_WRITE_ERROR,
            KTX_GL_ERROR,
            KTX_INVALID_OPERATION,
            KTX_INVALID_VALUE,
            KTX_NOT_FOUND,
            KTX_OUT_OF_MEMORY,
            KTX_TRANSCODE_FAILED,
            KTX_UNKNOWN_FILE_FORMAT,
            KTX_UNSUPPORTED_TEXTURE_TYPE,
            KTX_UNSUPPORTED_FEATURE,
            KTX_LIBRARY_NOT_LINKED,
            KTX_DECOMPRESS_LENGTH_ERROR,
            KTX_DECOMPRESS_CHECKSUM_ERROR,
            KTX_ERROR_MAX_ENUM = ktx_error_code_e.KTX_DECOMPRESS_CHECKSUM_ERROR,
        }

        public const string KTX_IDENTIFIER_REF = "{ 0xAB, 0x4B, 0x54, 0x58, 0x20, 0x31, 0x31, 0xBB, 0x0D, 0x0A, 0x1A, 0x0A }";
        public const int KTX_ENDIAN_REF = 67305985;
        public const int KTX_ENDIAN_REF_REV = 16909060;
        public const int KTX_HEADER_SIZE = 64;
        public const string KTX_APIENTRYP = "KTX_APIENTRY *";

        public enum ktxOrientationX
        {
            KTX_ORIENT_X_LEFT = 'l',
            KTX_ORIENT_X_RIGHT = 'r',
        }

        public enum ktxOrientationY
        {
            KTX_ORIENT_Y_UP = 'u',
            KTX_ORIENT_Y_DOWN = 'd',
        }

        public enum ktxOrientationZ
        {
            KTX_ORIENT_Z_IN = 'i',
            KTX_ORIENT_Z_OUT = 'o',
        }

        public enum class_id
        {
            ktxTexture1_c = 1,
            ktxTexture2_c = 2,
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct ktxOrientation
        {
            public ktxOrientationX x;
            public ktxOrientationY y;
            public ktxOrientationZ z;
        }

        public enum ktxSupercmpScheme
        {
            KTX_SS_NONE = 0,
            KTX_SS_BASIS_LZ = 1,
            KTX_SS_ZSTD = 2,
            KTX_SS_ZLIB = 3,
            KTX_SS_BEGIN_RANGE = KTX_SS_NONE,
            KTX_SS_END_RANGE = KTX_SS_ZLIB,
            KTX_SS_BEGIN_VENDOR_RANGE = 0x10000,
            KTX_SS_END_VENDOR_RANGE = 0x1ffff,
            KTX_SS_BEGIN_RESERVED = 0x20000,
            KTX_SUPERCOMPRESSION_BASIS = KTX_SS_BASIS_LZ,
            KTX_SUPERCOMPRESSION_ZSTD = KTX_SS_ZSTD
        }


        public enum ktxTextureCreateStorageEnum
        {
            KTX_TEXTURE_CREATE_NO_STORAGE = 0,
            KTX_TEXTURE_CREATE_ALLOC_STORAGE = 1
        }

        [Flags]
        public enum KtxTextureCreateFlagBits : ktx_uint32_t
        {
            KTX_TEXTURE_CREATE_NO_FLAGS = 0x00,
            KTX_TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT = 0x01,
            KTX_TEXTURE_CREATE_RAW_KVDATA_BIT = 0x02,
            KTX_TEXTURE_CREATE_SKIP_KVDATA_BIT = 0x04,
            KTX_TEXTURE_CREATE_CHECK_GLTF_BASISU_BIT = 0x08
        }



        public struct ktxTextureCreateInfo
        {
            public ktx_uint32_t glInternalformat;
            public ktx_uint32_t vkFormat;
            public ktx_uint32_t[] pDfd;
            public ktx_uint32_t baseWidth;
            public ktx_uint32_t baseHeight;
            public ktx_uint32_t baseDepth;
            public ktx_uint32_t numDimensions;
            public ktx_uint32_t numLevels;
            public ktx_uint32_t numLayers;
            public ktx_uint32_t numFaces;
            public ktx_bool_t isArray;
            public ktx_bool_t generateMipmaps;
        }

        public struct KtxStream
        {
            public delegate ktx_error_code_e ktxStream_read(KtxStream str, IntPtr dst, ktx_size_t count);
            public delegate ktx_error_code_e ktxStream_skip(KtxStream str,  ktx_size_t count);
            public delegate ktx_error_code_e ktxStream_write(KtxStream str, IntPtr src,  ktx_size_t size,  ktx_size_t count);
            public delegate ktx_error_code_e ktxStream_getpos(KtxStream str,  ref  ktx_off_t  offset);
            public delegate ktx_error_code_e ktxStream_setpos(KtxStream str,  ktx_off_t offset);
            public delegate ktx_error_code_e ktxStream_getsize(KtxStream str,  ref ktx_size_t size);
            public delegate void ktxStream_destruct(KtxStream str);

            public ktxStream_read read;
            public ktxStream_skip skip;
            public ktxStream_write write;
            public ktxStream_getpos getpos;
            public ktxStream_setpos setpos;
            public ktxStream_getsize getsize;
            public ktxStream_destruct destruct;
            public streamType type;
            public Stream file;
            public ktxMem mem;
            public IntPtr custom_ptr;
            public ktx_off_t readpos;
            public bool closeOnDestruct;
        }

        public struct ktxMem { }

        public enum streamType { eStreamTypeFile = 1, eStreamTypeMemory = 2, eStreamTypeCustom = 3 }

    }


    public class KtxTexture 
    {
       
        protected class_id classId;
        public ktxTexture_vtbl vtbl;
        protected ktxTexture_vvtbl vvtbl;
        protected ktxTexture_protected _protected;
        protected ktx_bool_t isArray;
        protected ktx_bool_t isCubemap;
        protected ktx_bool_t isCompressed;
        protected ktx_bool_t generateMipmaps;
        protected ktx_uint32_t baseWidth;
        protected ktx_uint32_t baseHeight;
        protected ktx_uint32_t baseDepth;
        protected ktx_uint32_t numDimensions;
        protected ktx_uint32_t numLevels;
        protected ktx_uint32_t numLayers;
        protected ktx_uint32_t numFaces;
        protected ktxOrientation orientation;
        // ktxHashList
        protected IntPtr kvDataHead;
        protected ktx_uint32_t kvDataLen;
        protected ktx_uint8_t kvData;
        protected ktx_size_t dataSize;
        protected ktx_uint8_t pData;


        [StructLayout(LayoutKind.Sequential)]
        public struct ktxTexture_vvtbl { }


        [StructLayout(LayoutKind.Sequential)]
        public struct ktxTexture_protected { }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ktx_error_code_e PFNKTXITERCB(int miplevel, int face, int width, int height, int depth, ktx_uint64_t faceLodSize, IntPtr pixels, IntPtr userdata);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void PFNKTEXDESTROY(IntPtr This);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ktx_error_code_e PFNKTEXGETIMAGEOFFSET(IntPtr This, ktx_uint32_t level, ktx_uint32_t layer, ktx_uint32_t faceSlice, IntPtr pOffset);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] 
        public delegate ktx_size_t PFNKTEXGETDATASIZEUNCOMPRESSED(IntPtr This);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ktx_size_t PFNKTEXGETIMAGESIZE(IntPtr This, ktx_uint32_t level);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ktx_error_code_e PFNKTEXITERATELEVELS(IntPtr This, PFNKTXITERCB iterCb, IntPtr userdata);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] 
        public delegate ktx_error_code_e PFNKTEXITERATELOADLEVELFACES(IntPtr This, PFNKTXITERCB iterCb, IntPtr userdata);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] 
        public delegate ktx_error_code_e PFNKTEXLOADIMAGEDATA(IntPtr This, ktx_uint8_t[] pBuffer, ktx_size_t bufSize);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] 
        public delegate ktx_bool_t PFNKTEXNEEDSTRANSCODING(IntPtr This);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] 
        public delegate ktx_error_code_e PFNKTEXSETIMAGEFROMMEMORY(IntPtr This, ktx_uint32_t level, ktx_uint32_t layer, ktx_uint32_t faceSlice, ktx_uint8_t[] src, ktx_size_t srcSize);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] 
        public delegate ktx_error_code_e PFNKTEXSETIMAGEFROMSTDIOSTREAM(IntPtr This, ktx_uint32_t level, ktx_uint32_t layer, ktx_uint32_t faceSlice, IntPtr src, ktx_size_t srcSize);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] 
        public delegate ktx_error_code_e PFNKTEXWRITETOSTDIOSTREAM(IntPtr This, IntPtr dstsstr);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] 
        public delegate ktx_error_code_e PFNKTEXWRITETONAMEDFILE(IntPtr This, IntPtr dstname);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ktx_error_code_e PFNKTEXWRITETOMEMORY(IntPtr This,  IntPtr bytes, IntPtr size);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] 
        public delegate ktx_error_code_e PFNKTEXWRITETOSTREAM(IntPtr This, IntPtr dststr);

        public struct ktxTexture_vtbl
        {
            public PFNKTEXDESTROY Destroy;
            public PFNKTEXGETIMAGEOFFSET GetImageOffset;
            public PFNKTEXGETDATASIZEUNCOMPRESSED GetDataSizeUncompressed;
            public PFNKTEXGETIMAGESIZE GetImageSize;
            public PFNKTEXITERATELEVELS IterateLevels;
            public PFNKTEXITERATELOADLEVELFACES IterateLoadLevelFaces;
            public PFNKTEXNEEDSTRANSCODING NeedsTranscoding;
            public PFNKTEXLOADIMAGEDATA LoadImageData;
            public PFNKTEXSETIMAGEFROMMEMORY SetImageFromMemory;
            public PFNKTEXSETIMAGEFROMSTDIOSTREAM SetImageFromStdioStream;
            public PFNKTEXWRITETOSTDIOSTREAM WriteToStdioStream;
            public PFNKTEXWRITETONAMEDFILE WriteToNamedFile;
            public PFNKTEXWRITETOMEMORY WriteToMemory;
            public PFNKTEXWRITETOSTREAM WriteToStream;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ktxTexture_vtbl_native
        {
            public IntPtr Destroy;
            public IntPtr GetImageOffset;
            public IntPtr GetDataSizeUncompressed;
            public IntPtr GetImageSize;
            public IntPtr IterateLevels;
            public IntPtr IterateLoadLevelFaces;
            public IntPtr NeedsTranscoding;
            public IntPtr LoadImageData;
            public IntPtr SetImageFromMemory;
            public IntPtr SetImageFromStdioStream;
            public IntPtr WriteToStdioStream;
            public IntPtr WriteToNamedFile;
            public IntPtr WriteToMemory;
            public IntPtr WriteToStream;
        }

        public static KtxTexture ktxTexture_MarshalFromPointer(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(ptr), "Pointer to native ktxTexture is null.");

            KtxTexture_native ktxnativeTexture = Marshal.PtrToStructure<KtxTexture_native>(ptr);

            KtxTexture outputText = new KtxTexture();

            outputText.classId = ktxnativeTexture.classId;

            if (ktxnativeTexture.vtbl != IntPtr.Zero)
            {
                outputText.vtbl = Marshal.PtrToStructure<ktxTexture_vtbl>(ktxnativeTexture.vtbl);
                PopulateVTable(ktxnativeTexture.vtbl);
            }

            if (ktxnativeTexture.vvtbl != IntPtr.Zero)
            {
                outputText.vvtbl = Marshal.PtrToStructure<ktxTexture_vvtbl>(ktxnativeTexture.vvtbl);
            }

            if (ktxnativeTexture._protected != IntPtr.Zero)
            {
                outputText._protected = Marshal.PtrToStructure<ktxTexture_protected>(ktxnativeTexture._protected);
            }

            outputText.isArray = ktxnativeTexture.isArray;
            outputText.isCubemap = ktxnativeTexture.isCubemap;
            outputText.isCompressed = ktxnativeTexture.isCompressed;
            outputText.generateMipmaps = ktxnativeTexture.generateMipmaps;
            outputText.baseWidth = ktxnativeTexture.baseWidth;
            outputText.baseHeight = ktxnativeTexture.baseHeight;
            outputText.baseDepth = ktxnativeTexture.baseDepth;
            outputText.numDimensions = ktxnativeTexture.numDimensions;
            outputText.numLevels = ktxnativeTexture.numLevels;
            outputText.numLayers = ktxnativeTexture.numLayers;
            outputText.numFaces = ktxnativeTexture.numFaces;
            outputText.orientation = ktxnativeTexture.orientation;
            outputText.kvDataHead = ktxnativeTexture.kvDataHead;
            outputText.kvDataLen = ktxnativeTexture.kvDataLen;
            outputText.kvData = ktxnativeTexture.kvData;
            outputText.dataSize = ktxnativeTexture.dataSize;
            outputText.pData = ktxnativeTexture.pData;

            return outputText;
        }
        public static ktxTexture_vtbl PopulateVTable(IntPtr vtblPtr)
        {
            if (vtblPtr == IntPtr.Zero)
                throw new ArgumentException("vtblPtr cannot be IntPtr.Zero.");


              var vtblNative = Marshal.PtrToStructure<ktxTexture_vtbl_native>(vtblPtr);

            ktxTexture_vtbl _vtblManaged = new ktxTexture_vtbl
            {
                   Destroy = Marshal.GetDelegateForFunctionPointer<PFNKTEXDESTROY>(vtblNative.Destroy),
                   GetImageOffset = Marshal.GetDelegateForFunctionPointer<PFNKTEXGETIMAGEOFFSET>(vtblNative.GetImageOffset),
                   GetDataSizeUncompressed = Marshal.GetDelegateForFunctionPointer<PFNKTEXGETDATASIZEUNCOMPRESSED>(vtblNative.GetDataSizeUncompressed),
                   GetImageSize = Marshal.GetDelegateForFunctionPointer<PFNKTEXGETIMAGESIZE>(vtblNative.GetImageSize),
                   IterateLevels = Marshal.GetDelegateForFunctionPointer<PFNKTEXITERATELEVELS>(vtblNative.IterateLevels),
                   IterateLoadLevelFaces = Marshal.GetDelegateForFunctionPointer<PFNKTEXITERATELOADLEVELFACES>(vtblNative.IterateLoadLevelFaces),
                   NeedsTranscoding = Marshal.GetDelegateForFunctionPointer<PFNKTEXNEEDSTRANSCODING>(vtblNative.NeedsTranscoding),
                   LoadImageData = Marshal.GetDelegateForFunctionPointer<PFNKTEXLOADIMAGEDATA>(vtblNative.LoadImageData),
                   SetImageFromMemory = Marshal.GetDelegateForFunctionPointer<PFNKTEXSETIMAGEFROMMEMORY>(vtblNative.SetImageFromMemory),
                   SetImageFromStdioStream = Marshal.GetDelegateForFunctionPointer<PFNKTEXSETIMAGEFROMSTDIOSTREAM>(vtblNative.SetImageFromStdioStream),
                   WriteToStdioStream = Marshal.GetDelegateForFunctionPointer<PFNKTEXWRITETOSTDIOSTREAM>(vtblNative.WriteToStdioStream),
                   WriteToNamedFile = Marshal.GetDelegateForFunctionPointer<PFNKTEXWRITETONAMEDFILE>(vtblNative.WriteToNamedFile),
                   WriteToMemory = Marshal.GetDelegateForFunctionPointer<PFNKTEXWRITETOMEMORY>(vtblNative.WriteToMemory),
                   WriteToStream = Marshal.GetDelegateForFunctionPointer<PFNKTEXWRITETOSTREAM>(vtblNative.WriteToStream)
               };

            return _vtblManaged;
        }
    }

    public class KtxTexture1 : KtxTexture
    {
        public ktx_uint32_t glFormat;
        public ktx_uint32_t glInternalformat;
        public ktx_uint32_t glBaseInternalformat;
        public ktx_uint32_t glType;
        public ktxTexture1_private _private;


        public struct ktxTexture1_private { }
    }

    
    public class ktxTexture2 : KtxTexture
    {
        public ktx_uint32_t vkFormat;
        public ktx_uint32_t[] pDfd;
        public ktxSupercmpScheme supercompressionScheme;
        public ktx_bool_t isVideo;
        public ktx_uint32_t duration;
        public ktx_uint32_t timescale;
        public ktx_uint32_t loopcount;
        public ktxTexture2_private _private;

        public struct ktxTexture2_private { }

        public static ktxTexture2 ktxTexture2_MarshalFromPointer(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(ptr), "Pointer to native ktxTexture is null.");

            KtxTexture_native ktxnativeTexture = Marshal.PtrToStructure<KtxTexture_native>(ptr);

            ktxTexture2 outputText = new ktxTexture2();

            outputText.classId = ktxnativeTexture.classId;

            if (ktxnativeTexture.vtbl != IntPtr.Zero)
            {
                outputText.vtbl =  PopulateVTable(ktxnativeTexture.vtbl);
            }

            if (ktxnativeTexture.vvtbl != IntPtr.Zero)
            {
                outputText.vvtbl = Marshal.PtrToStructure<ktxTexture_vvtbl>(ktxnativeTexture.vvtbl);
            }

            if (ktxnativeTexture._protected != IntPtr.Zero)
            {
                outputText._protected = Marshal.PtrToStructure<ktxTexture_protected>(ktxnativeTexture._protected);
            }

            outputText.isArray = ktxnativeTexture.isArray;
            outputText.isCubemap = ktxnativeTexture.isCubemap;
            outputText.isCompressed = ktxnativeTexture.isCompressed;
            outputText.generateMipmaps = ktxnativeTexture.generateMipmaps;
            outputText.baseWidth = ktxnativeTexture.baseWidth;
            outputText.baseHeight = ktxnativeTexture.baseHeight;
            outputText.baseDepth = ktxnativeTexture.baseDepth;
            outputText.numDimensions = ktxnativeTexture.numDimensions;
            outputText.numLevels = ktxnativeTexture.numLevels;
            outputText.numLayers = ktxnativeTexture.numLayers;
            outputText.numFaces = ktxnativeTexture.numFaces;
            outputText.orientation = ktxnativeTexture.orientation;
            outputText.kvDataHead = ktxnativeTexture.kvDataHead;
            outputText.kvDataLen = ktxnativeTexture.kvDataLen;
            outputText.kvData = ktxnativeTexture.kvData;
            outputText.dataSize = ktxnativeTexture.dataSize;
            outputText.pData = ktxnativeTexture.pData;


            ktxTexture2_native ktx2nativeTexture = Marshal.PtrToStructure<ktxTexture2_native>(ptr);
    
            outputText.vkFormat = ktx2nativeTexture.vkFormat;
            outputText.pDfd = ktx2nativeTexture.pDfd;
            outputText.supercompressionScheme = ktx2nativeTexture.supercompressionScheme;
            outputText.isVideo  = ktx2nativeTexture.isVideo;
            outputText.duration = ktx2nativeTexture.duration;
            outputText.timescale = ktx2nativeTexture.timescale;
            outputText.loopcount = ktx2nativeTexture.loopcount;
            
            return outputText;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KtxTexture_native
    {
        public class_id classId;
        public IntPtr vtbl;
        public IntPtr vvtbl;
        public IntPtr _protected; 
        public ktx_bool_t isArray;
        public ktx_bool_t isCubemap;
        public ktx_bool_t isCompressed;
        public ktx_bool_t generateMipmaps;
        public ktx_uint32_t baseWidth;
        public ktx_uint32_t baseHeight;
        public ktx_uint32_t baseDepth;
        public ktx_uint32_t numDimensions;
        public ktx_uint32_t numLevels;
        public ktx_uint32_t numLayers;
        public ktx_uint32_t numFaces;
        public ktxOrientation orientation;
        public IntPtr kvDataHead;
        public ktx_uint32_t kvDataLen;
        public ktx_uint8_t kvData;
        public ktx_size_t dataSize;
        public ktx_uint8_t pData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ktxTexture2_native
    {
        public ktx_uint32_t vkFormat;

        [MarshalAs(UnmanagedType.ByValArray)]
        public ktx_uint32_t[] pDfd;

        [MarshalAs(UnmanagedType.U4)]
        public ktxSupercmpScheme supercompressionScheme;
        public ktx_bool_t isVideo;
        public ktx_uint32_t duration;
        public ktx_uint32_t timescale;
        public ktx_uint32_t loopcount;
        public IntPtr _private;
    }
}
