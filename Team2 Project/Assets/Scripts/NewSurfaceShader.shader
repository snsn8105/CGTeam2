Shader "Custom/TwoSidedTileShader"

{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Cull Off

        UsePass "Standard/BASE"
        UsePass "Standard/FORWARD"
        UsePass "Standard/DEFERRED"
    }
}
