Shader "Unlit/Addative"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        // The rest of the code that defines the SubShader goes here.

        Pass
        {    
            // Enable regular alpha blending for this Pass
            Blend One One
            
            // The rest of the code that defines the Pass goes here.
        }
    }
}