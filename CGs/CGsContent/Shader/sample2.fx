float4x4 World;
float4x4 View;
float4x4 Projection;
texture tex0;

sampler mySampler = sampler_state {
	Texture = <tex0>;
};

struct VertexPositionTexture
{
    float4 Position : POSITION0;
	float4 TextureCoordinate : TEXCOORD;
};

VertexPositionTexture VertexShaderFunction(VertexPositionTexture input)
{
    VertexPositionTexture output;
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.TextureCoordinate = input.TextureCoordinate;

    return output;
}

float4 PixelShaderFunction(float2 textureCoordinate : TEXCOORD) : COLOR0
{
    return tex2D(mySampler, textureCoordinate);
}

technique Render
{
    pass P0
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
		Sampler[0] = (mySampler);
    }
}