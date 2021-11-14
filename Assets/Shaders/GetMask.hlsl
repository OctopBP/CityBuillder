half4 _GridSize = half4(0, 0, 0, 0);
float _Cells[1000];

void GetMask_float(float3 UV, float2 Size, out float Out)
{
	float xIndex = floor(UV.r * Size.x);
	float yIndex = floor(UV.g * Size.y);

	Out = 0;
	for (int i = 0; i < Size.x * Size.y; i++)
	{
		if (xIndex * Size.y + yIndex + 0.1 > i && xIndex * Size.y + yIndex - 0.1 < i)
			Out = _Cells[i];
	}
}
