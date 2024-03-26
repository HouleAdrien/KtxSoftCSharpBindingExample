This is a simple example of how you could bind to the ktx-software lib in c# that works on windows using the dll of https://github.com/KhronosGroup/KTX-Software .
It was made for personnal use, and I WILL NOT maintain it.
The only objective I had was to be able to read/write ktx2 in c#, and convert a PNG to ktx2.
All the code is totally free of use.
If you want to help evolves this make a pr.

If you want to test this you need to modify the path to the dll in KTXBindingsFunctions.cs.
And also modify the input and output path foreach test in the KTX2test.cs file.
