using CSharpKtxSoftwareBindings;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using static CSharpKtxSoftwareBindings.KTXBindingsFunctions;
using static CSharpKtxSoftwareBindings.KTXBindingsTypes;
using static CSharpKtxSoftwareBindings.KtxTexture;
using System.IO.Compression;
using System.Diagnostics;


namespace Obj2Tiles
{
    internal class Ktx2Tests
    {

        public static void TestKtx2Interop()
        {
            int totalSuccess = 0;
            int totalFailures = 0;
            int totalTestNumber = 0;


            Console.WriteLine("---------------------Start of tests-------------------------");
            Console.WriteLine();
            Console.WriteLine("---------------------Test 1-------------------------");
            Console.WriteLine();

            bool test1res = TestKtx2ReadCopyWriteFile();
            totalTestNumber++;

            if (test1res)
            {
                Console.WriteLine("---------------------Test 1 succeeded---------------------");
                Console.WriteLine();
                totalSuccess++;
            }
            else
            {
                Console.WriteLine("---------------------Test 1 failed---------------------");
                Console.WriteLine();
                totalFailures++;
            }

            Console.WriteLine();
            Console.WriteLine("---------------------Test 2-------------------------");
            Console.WriteLine();

            bool test2res = TestCreateKTX2FromPNG();
            totalTestNumber++;

            if (test2res)
            {
                Console.WriteLine("---------------------Test 2 succeeded---------------------");
                Console.WriteLine();
                totalSuccess++;
            }
            else
            {
                Console.WriteLine("---------------------Test 2 failed---------------------");
                Console.WriteLine();
                totalFailures++;
            }


            Console.WriteLine("---------------------Tests results---------------------");
            Console.WriteLine($"Number of tests: {totalTestNumber} ");
            Console.WriteLine($"Number of tests succeeded : {totalSuccess} ");
            Console.WriteLine($"Number of tests failed : {totalFailures} ");

            Console.WriteLine("---------------------End of tests-------------------------");
        }

        //Simple Test that read a ktx2 texture copies it and write in back in an other ktx2 file
        private static bool TestKtx2ReadCopyWriteFile()
        {
            string inputFilePath = @"C:\TestInterop\KtxSoftCSharpBindingExample\testktx2\inputTest1.ktx2";
            string outputFilePath = @"C:\TestInterop\KtxSoftCSharpBindingExample\testktx2\resultTest1.ktx2";

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"The file {inputFilePath} does not exist.");
                return false;
            }

            IntPtr inputPathPtr = Marshal.StringToHGlobalAnsi(inputFilePath);
            IntPtr outputPathPtr = Marshal.StringToHGlobalAnsi(outputFilePath);

            // Allocate memory for a pointer. This will act as ktxTexture2**.
            IntPtr ptrToKtxTexture2Ptr = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(ptrToKtxTexture2Ptr, IntPtr.Zero); // Initialize with NULL

            // Call the native method, expecting it to allocate a ktxTexture2 instance and populate ptrToKtxTexture2Ptr.
            ktx_error_code_e resultCode = ktxTexture2_CreateFromNamedFile(
                inputPathPtr,
                KtxTextureCreateFlagBits.KTX_TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT.Cast<System.UInt32>(),
                ptrToKtxTexture2Ptr);

            if (resultCode != ktx_error_code_e.KTX_SUCCESS)
            {
                Console.WriteLine("Failed to create a texture from the file. " + resultCode);
                Marshal.FreeHGlobal(outputPathPtr);
                Marshal.FreeHGlobal(inputPathPtr);
                return false;
            }
            else
            {
                Console.WriteLine("Texture created from file successfully.");

                IntPtr ktxTexture2Ptr = Marshal.ReadIntPtr(ptrToKtxTexture2Ptr);

                if (ktxTexture2Ptr != IntPtr.Zero)
                {
                    // Here the native copy is useless and here for test
                    IntPtr ptrToCopiedKtxTexture2Ptr = Marshal.AllocHGlobal(IntPtr.Size);
                    Marshal.WriteIntPtr(ptrToCopiedKtxTexture2Ptr, IntPtr.Zero);

                    resultCode = ktxTexture2_CreateCopy(ktxTexture2Ptr, ptrToCopiedKtxTexture2Ptr);

                    if (resultCode != ktx_error_code_e.KTX_SUCCESS)
                    {
                        Console.WriteLine("Failed to copy the texture.");
                        Marshal.FreeHGlobal(outputPathPtr);
                        Marshal.FreeHGlobal(inputPathPtr);
                        ktxTexture2 managedTexture = ktxTexture2.ktxTexture2_MarshalFromPointer(ktxTexture2Ptr);
                        managedTexture.vtbl.Destroy(ktxTexture2Ptr);
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Texture copy created successfully.");
                        IntPtr copiedTexturePtr = Marshal.ReadIntPtr(ptrToCopiedKtxTexture2Ptr);
                        ktxTexture2 managedTexture = ktxTexture2.ktxTexture2_MarshalFromPointer(copiedTexturePtr);

                        resultCode = managedTexture.vtbl.WriteToNamedFile(copiedTexturePtr, outputPathPtr);

                        if (resultCode == ktx_error_code_e.KTX_SUCCESS)
                        {

                            Console.WriteLine("Texture written successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Texture write failed.");
                            Marshal.FreeHGlobal(outputPathPtr);
                            Marshal.FreeHGlobal(inputPathPtr);
                            return false;
                        }

                        managedTexture.vtbl.Destroy(copiedTexturePtr);
                    }

                    ktxTexture2 managedTexture1 = ktxTexture2.ktxTexture2_MarshalFromPointer(ktxTexture2Ptr);
                    managedTexture1.vtbl.Destroy(ktxTexture2Ptr);
                }
            }

            Marshal.FreeHGlobal(outputPathPtr);
            Marshal.FreeHGlobal(inputPathPtr);
            return true;
        }

        private static bool TestCreateKTX2FromPNG()
        {
            string inputFilePath = @"C:\TestInterop\KtxSoftCSharpBindingExample\testktx2\inputTest2.png";
            string outputFilePath = @"C:\TestInterop\KtxSoftCSharpBindingExample\testktx2\resultTest2.ktx2";

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"The file {inputFilePath} does not exist.");
                return false;
            }


            byte[] imageData = null;
            int width, height;

            try
            {
                // Load image using ImageSharp
                using (var image = Image.Load<Rgba32>(inputFilePath))
                {
                    width = image.Width;
                    height = image.Height;

                    imageData = ImageToByteArray(image);
                }
                Console.WriteLine("Input image successfully loaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load the input image. Error: {ex.Message}");

                return false;
            }

            var createInfo = new ktxTextureCreateInfo
            {
                // VK_FORMAT_R8G8B8A8_UNORM
                vkFormat = 37,
                baseWidth = (uint)width,
                baseHeight = (uint)height,
                baseDepth = 1,
                numDimensions = 2,
                numLevels = 1, // Assuming no mipmaps for simplicity
                numLayers = 1,
                numFaces = 1,
                isArray = ktx_bool_t.KTX_FALSE,
                generateMipmaps = ktx_bool_t.KTX_FALSE,

            };

            IntPtr ptrToKtxTexture2 = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(ptrToKtxTexture2, IntPtr.Zero);

            ktx_error_code_e resultCode = ktxTexture2_Create(createInfo, ktxTextureCreateStorageEnum.KTX_TEXTURE_CREATE_ALLOC_STORAGE, ptrToKtxTexture2);

            if (resultCode != ktx_error_code_e.KTX_SUCCESS)
            {
                Console.WriteLine("Failed to create a empty texture to host input image data. " + resultCode);
                return false;
            }
            else
            {
                IntPtr outputPathPtr = Marshal.StringToHGlobalAnsi(outputFilePath);

                IntPtr texptr = Marshal.ReadIntPtr(ptrToKtxTexture2);

                ktxTexture2 managedTexture = ktxTexture2.ktxTexture2_MarshalFromPointer(texptr);

                managedTexture.vtbl.SetImageFromMemory(texptr, 0, 0, 0, imageData, imageData.Length);

                ktxBasisParams compressParams = new ktxBasisParams();
                compressParams.threadCount = 14;
                compressParams.compressionLevel = 2;
                compressParams.qualityLevel = 128;

                IntPtr paramsptr = Marshal.AllocHGlobal(Marshal.SizeOf(compressParams));
                Marshal.StructureToPtr(compressParams, paramsptr, false);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                resultCode = ktxTexture2_CompressBasisEx(texptr, paramsptr);

                stopwatch.Stop();

                Console.WriteLine($"Compression time: {stopwatch.Elapsed:hh\\:mm\\:ss}");



                if (resultCode == ktx_error_code_e.KTX_SUCCESS)
                {

                    Console.WriteLine("Input image data copied successfully.");
                    resultCode = managedTexture.vtbl.WriteToNamedFile(texptr, outputPathPtr);
                    if (resultCode != ktx_error_code_e.KTX_SUCCESS)
                    {

                        Console.WriteLine("Texture write failed.");
                        managedTexture.vtbl.Destroy(texptr);
                        Marshal.FreeHGlobal(paramsptr);
                        Marshal.FreeHGlobal(outputPathPtr);
                        return false;
                    }

                    Marshal.FreeHGlobal(outputPathPtr);
                    Marshal.FreeHGlobal(paramsptr);
                    managedTexture.vtbl.Destroy(texptr);
                }
                else
                {
                    Console.WriteLine("Input image data copy failed." + resultCode);
                    Marshal.FreeHGlobal(paramsptr);
                    Marshal.FreeHGlobal(outputPathPtr);
                    managedTexture.vtbl.Destroy(texptr);
                    return false;
                }
            }

            return true;
        }

        private static byte[] ImageToByteArray(Image<Rgba32> image)
        {
            var pixelData = new byte[image.Width * image.Height * 4]; // 4 bytes per pixel for R, G, B, A
            int byteIndex = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image[x, y];
                    pixelData[byteIndex++] = pixel.R;
                    pixelData[byteIndex++] = pixel.G;
                    pixelData[byteIndex++] = pixel.B;
                    pixelData[byteIndex++] = pixel.A;
                }
            }
            return pixelData;
        }


    }
}
