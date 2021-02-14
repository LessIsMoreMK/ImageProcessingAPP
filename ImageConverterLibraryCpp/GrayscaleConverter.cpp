#include "pch.h"
#include "GrayscaleConverter.h"

#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <math.h>

inline BYTE GrayscaleCpp(int* r, int* g, int* b) { return ((*r) + (*g) + (*b)); }

extern "C" __declspec(dllexport) HBITMAP GrayscaleCpp(HBITMAP bmp) {
#pragma region Local variables
	INT x = 0, y = 0;
	char Gray;
	BITMAP bm;
#pragma endregion

#pragma region Activating HBITMAP
	GetObject(bmp, sizeof(BITMAP), (LPSTR)&bm);
#pragma endregion

#pragma region Assigning pointer
	BYTE* pImgByte = (BYTE*)bm.bmBits;
	BYTE* fpImgByte = (BYTE*)malloc(bm.bmHeight * bm.bmWidth * sizeof(BYTE));
	if (fpImgByte == nullptr) { printf("ERROR: Unable to allocate memory for buffer array!"); return nullptr; }
	INT iWidthBytes = bm.bmWidth * 4;
#pragma endregion

#pragma region TrueColor to GrayScale
	for (y = 0; y < bm.bmHeight; y++) {
		for (x = 0; x < bm.bmWidth; x++) {

			int red = pImgByte[y * iWidthBytes + x * 4 + 3];
			int blue = pImgByte[y * iWidthBytes + x * 4 + 2];
			int green = pImgByte[y * iWidthBytes + x * 4 + 1];

			if (red > blue && red > green)
				red = 0;
			else if (green > blue && green > red)
				green = 0;
			else
				blue = 0;

			Gray = GrayscaleCpp(&red, &green, &blue);
			fpImgByte[y * bm.bmWidth + x] = Gray;
		}
	}
#pragma endregion

#pragma region Flipping bitmap
	for (y = 0; y < bm.bmHeight; y++) {
		for (x = 0; x < bm.bmWidth; x++) {
			Gray = fpImgByte[(bm.bmHeight - y - 1) * bm.bmWidth + x];
			pImgByte[y * iWidthBytes + x * 4] = Gray;
			pImgByte[y * iWidthBytes + x * 4 + 1] = Gray;
			pImgByte[y * iWidthBytes + x * 4 + 2] = Gray;
			pImgByte[y * iWidthBytes + x * 4 + 3] = Gray;
		}
	}
#pragma endregion 

#pragma region Releasing memory
	free(fpImgByte);
#pragma endregion

#pragma region Returning converted bitmap
	return CreateBitmapIndirect(&bm);
#pragma endregion
}