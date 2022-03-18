xof 0303txt 0032
template XSkinMeshHeader {
 <3cf169ce-ff7c-44ab-93c0-f78f62d172e2>
 WORD nMaxSkinWeightsPerVertex;
 WORD nMaxSkinWeightsPerFace;
 WORD nBones;
}

template VertexDuplicationIndices {
 <b8d65549-d7c9-4995-89cf-53a9a8b031e3>
 DWORD nIndices;
 DWORD nOriginalVertices;
 array DWORD indices[nIndices];
}

template SkinWeights {
 <6f0d123b-bad2-4167-a0d0-80224f25fabb>
 STRING transformNodeName;
 DWORD nWeights;
 array DWORD vertexIndices[nWeights];
 array FLOAT weights[nWeights];
 Matrix4x4 matrixOffset;
}

template FVFData {
 <b6e70a0e-8ef9-4e83-94ad-ecc8b0c04897>
 DWORD dwFVF;
 DWORD nDWords;
 array DWORD data[nDWords];
}

template EffectInstance {
 <e331f7e4-0559-4cc2-8e99-1cec1657928f>
 STRING EffectFilename;
 [...]
}

template EffectParamFloats {
 <3014b9a0-62f5-478c-9b86-e4ac9f4e418b>
 STRING ParamName;
 DWORD nFloats;
 array FLOAT Floats[nFloats];
}

template EffectParamString {
 <1dbc4c88-94c1-46ee-9076-2c28818c9481>
 STRING ParamName;
 STRING Value;
}

template EffectParamDWord {
 <e13963bc-ae51-4c5d-b00f-cfa3a9d97ce5>
 STRING ParamName;
 DWORD Value;
}


Material skyscrapers {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "Skyscrp.tga";
 }
}

Material buildz1 {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "buildz1.tga";
 }
}

Material buildz3 {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "buildz3.tga";
 }
}

Material buildz4 {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "buildz4.tga";
 }
}

Material buildz5 {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "buildz5.tga";
 }
}

Material buildz6 {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "buildz6.tga";
 }
}

Material buildz2 {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "buildz2.tga";
 }
}

Material roads {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "roads.tga";
 }
}

Material roads-2 {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "roads2.tga";
 }
}

Material tree-01 {
 1.000000;1.000000;1.000000;1.000000;;
 3.200000;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;

 TextureFilename {
  "trees-02.tga";
 }
}

Frame building-industrial_03 {
 

 FrameTransformMatrix {
  1.000000,0.000000,0.000000,0.000000,0.000000,-0.000000,-1.000000,0.000000,0.000000,1.000000,-0.000000,0.000000,-1.480856,-12.670539,12.491668,1.000000;;
 }

 Frame {
  

  FrameTransformMatrix {
   1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,-0.000000,0.000000,1.000000;;
  }

  Mesh  {
   204;
   13.810907;6.587498;19.729128;,
   -9.454890;6.587499;19.729128;,
   -12.428114;1.217629;15.300939;,
   -12.428114;1.217629;15.300939;,
   17.034790;1.217634;15.300939;,
   13.810907;6.587498;19.729128;,
   -12.428115;6.587499;15.300939;,
   -12.428116;11.599509;15.300939;,
   -12.428116;11.599509;0.000000;,
   -12.428115;6.587499;15.300939;,
   -12.428116;11.599509;0.000000;,
   -12.428114;1.217629;0.000000;,
   -12.428115;6.587499;15.300939;,
   -12.428114;1.217629;0.000000;,
   -12.428114;1.217629;15.300939;,
   17.034786;11.599510;15.300939;,
   -12.428116;11.599509;15.300939;,
   -9.454890;6.587499;19.729128;,
   -9.454890;6.587499;19.729128;,
   13.810907;6.587498;19.729128;,
   17.034786;11.599510;15.300939;,
   -12.428116;11.599509;15.300939;,
   -12.428115;6.587499;15.300939;,
   -9.454890;6.587499;19.729128;,
   -9.454890;6.587499;19.729128;,
   -12.428115;6.587499;15.300939;,
   -12.428114;1.217629;15.300939;,
   17.034790;1.217634;15.300939;,
   17.034786;6.587499;15.300939;,
   13.810907;6.587498;19.729128;,
   13.810907;6.587498;19.729128;,
   17.034786;6.587499;15.300939;,
   17.034786;11.599510;15.300939;,
   17.034786;11.599510;0.000000;,
   17.034786;11.599510;15.300939;,
   17.034786;6.587499;15.300939;,
   17.034786;6.587499;15.300939;,
   17.034790;1.217634;15.300939;,
   17.034790;1.217634;0.000000;,
   17.034786;11.599510;0.000000;,
   17.034786;6.587499;15.300939;,
   17.034790;1.217634;0.000000;,
   -2.425118;11.599511;6.536368;,
   -2.425118;11.599511;0.000000;,
   -12.428116;11.599509;0.000000;,
   -2.425118;11.599511;6.536368;,
   -12.428116;11.599509;0.000000;,
   -12.428116;11.599509;15.300939;,
   6.792984;11.599513;6.536368;,
   -2.425118;11.599511;6.536368;,
   -12.428116;11.599509;15.300939;,
   6.792984;11.599513;6.536368;,
   -12.428116;11.599509;15.300939;,
   17.034786;11.599510;15.300939;,
   6.792984;11.599513;6.536368;,
   17.034786;11.599510;15.300939;,
   17.034786;11.599510;0.000000;,
   6.792984;11.599513;6.536368;,
   17.034786;11.599510;0.000000;,
   6.792983;11.599513;0.000000;,
   6.792984;1.213401;6.536368;,
   6.792985;1.213401;-0.000000;,
   17.034790;1.217634;0.000000;,
   6.792984;1.213401;6.536368;,
   17.034790;1.217634;0.000000;,
   17.034790;1.217634;15.300939;,
   6.792984;1.213401;6.536368;,
   17.034790;1.217634;15.300939;,
   -12.428114;1.217629;15.300939;,
   -2.425116;1.217631;6.536368;,
   6.792984;1.213401;6.536368;,
   -12.428114;1.217629;15.300939;,
   -2.425116;1.217631;6.536368;,
   -12.428114;1.217629;15.300939;,
   -12.428114;1.217629;0.000000;,
   -2.425116;1.217631;6.536368;,
   -12.428114;1.217629;0.000000;,
   -2.425117;1.213400;-0.000000;,
   -2.425118;1.209159;6.536368;,
   -2.425117;1.213400;-0.000000;,
   -2.425118;11.599511;0.000000;,
   -2.425118;11.599511;0.000000;,
   -2.425118;11.599511;6.536368;,
   -2.425118;1.209159;6.536368;,
   6.792984;1.213401;6.536368;,
   -2.425118;1.209159;6.536368;,
   -2.425118;11.599511;6.536368;,
   -2.425118;11.599511;6.536368;,
   6.792984;11.599513;6.536368;,
   6.792984;1.213401;6.536368;,
   6.792985;1.213401;-0.000000;,
   6.792984;1.213401;6.536368;,
   6.792984;11.599513;6.536368;,
   6.792984;11.599513;6.536368;,
   6.792983;11.599513;0.000000;,
   6.792985;1.213401;-0.000000;,
   -12.123407;2.513665;-0.000000;,
   -12.123407;2.513665;5.993334;,
   -22.505274;2.513664;5.993334;,
   -22.505274;2.513664;5.993334;,
   -22.505274;2.513664;-0.000000;,
   -12.123407;2.513665;-0.000000;,
   -22.505278;10.144632;5.993334;,
   -22.505278;10.148369;-0.000000;,
   -22.505274;2.513664;-0.000000;,
   -22.505274;2.513664;-0.000000;,
   -22.505274;2.513664;5.993334;,
   -22.505278;10.144632;5.993334;,
   -12.123407;2.513665;5.993334;,
   -12.123407;2.513665;-0.000000;,
   -12.123406;10.108507;-0.000000;,
   -12.123406;10.108507;-0.000000;,
   -12.149496;10.143601;5.993334;,
   -12.123407;2.513665;5.993334;,
   -22.505274;2.513664;5.993334;,
   -12.123407;2.513665;5.993334;,
   -12.149496;10.143601;5.993334;,
   -12.149496;10.143601;5.993334;,
   -22.505278;10.144632;5.993334;,
   -22.505274;2.513664;5.993334;,
   -12.149496;10.143601;5.993334;,
   -12.175591;10.149109;-0.000000;,
   -22.505278;10.148369;-0.000000;,
   -22.505278;10.148369;-0.000000;,
   -22.505278;10.144632;5.993334;,
   -12.149496;10.143601;5.993334;,
   22.505312;-5.343362;0.000000;,
   22.505310;5.720391;0.000000;,
   22.505310;5.720391;5.004097;,
   22.505312;-5.343362;0.000000;,
   22.505310;5.720391;5.004097;,
   22.505312;-5.343362;5.004097;,
   9.683117;5.720380;5.004097;,
   22.505310;5.720391;5.004097;,
   22.505310;5.720391;0.000000;,
   22.505310;5.720391;0.000000;,
   9.683117;5.720380;0.000000;,
   9.683117;5.720380;5.004097;,
   9.683117;5.720380;0.000000;,
   9.683117;-5.343364;0.000000;,
   9.683117;-5.343364;5.004097;,
   9.683117;5.720380;0.000000;,
   9.683117;-5.343364;5.004097;,
   9.683117;5.720380;5.004097;,
   22.505312;-5.343362;5.004097;,
   9.683117;-5.343364;5.004097;,
   9.683117;-5.343364;0.000000;,
   9.683117;-5.343364;0.000000;,
   22.505312;-5.343362;0.000000;,
   22.505312;-5.343362;5.004097;,
   22.505310;5.720391;5.004097;,
   9.683117;5.720380;5.004097;,
   9.683117;-5.343364;5.004097;,
   9.683117;-5.343364;5.004097;,
   22.505312;-5.343362;5.004097;,
   22.505310;5.720391;5.004097;,
   -4.632794;-11.599488;0.000000;,
   -4.632794;-11.599488;6.478696;,
   -11.333097;-11.599481;6.478696;,
   -4.632794;-11.599488;0.000000;,
   -11.333097;-11.599481;6.478696;,
   -11.333097;-11.599481;0.000000;,
   -11.333097;-11.599481;0.000000;,
   -11.333097;-11.599481;6.478696;,
   -11.333097;1.409288;6.478696;,
   -11.333097;1.409288;6.478696;,
   -11.333097;1.413821;0.000000;,
   -11.333097;-11.599481;0.000000;,
   -4.632794;1.365478;0.000000;,
   -4.632794;1.401345;6.478696;,
   -4.632794;-11.599488;6.478696;,
   -4.632794;-11.599488;6.478696;,
   -4.632794;-11.599488;0.000000;,
   -4.632794;1.365478;0.000000;,
   -4.632794;1.401345;6.478696;,
   -11.333097;1.409288;6.478696;,
   -11.333097;-11.599481;6.478696;,
   -11.333097;-11.599481;6.478696;,
   -4.632794;-11.599488;6.478696;,
   -4.632794;1.401345;6.478696;,
   12.527589;1.266618;15.000261;,
   12.527596;0.245328;14.422274;,
   12.527596;0.245328;4.934875;,
   12.527596;0.245328;4.934875;,
   12.527596;1.266618;4.934875;,
   12.527589;1.266618;15.000261;,
   10.702784;0.245328;14.422274;,
   10.702778;1.266617;15.000261;,
   10.702784;1.266617;4.934875;,
   10.702784;1.266617;4.934875;,
   10.702784;0.245328;4.934875;,
   10.702784;0.245328;14.422274;,
   12.527596;0.245328;14.422274;,
   10.702784;0.245328;14.422274;,
   10.702784;0.245328;4.934875;,
   10.702784;0.245328;4.934875;,
   12.527596;0.245328;4.934875;,
   12.527596;0.245328;14.422274;,
   10.702778;1.266617;15.000261;,
   10.702784;0.245328;14.422274;,
   12.527596;0.245328;14.422274;,
   12.527596;0.245328;14.422274;,
   12.527589;1.266618;15.000261;,
   10.702778;1.266617;15.000261;;
   68;
   3;0,1,2;,
   3;3,4,5;,
   3;6,7,8;,
   3;9,10,11;,
   3;12,13,14;,
   3;15,16,17;,
   3;18,19,20;,
   3;21,22,23;,
   3;24,25,26;,
   3;27,28,29;,
   3;30,31,32;,
   3;33,34,35;,
   3;36,37,38;,
   3;39,40,41;,
   3;42,43,44;,
   3;45,46,47;,
   3;48,49,50;,
   3;51,52,53;,
   3;54,55,56;,
   3;57,58,59;,
   3;60,61,62;,
   3;63,64,65;,
   3;66,67,68;,
   3;69,70,71;,
   3;72,73,74;,
   3;75,76,77;,
   3;78,79,80;,
   3;81,82,83;,
   3;84,85,86;,
   3;87,88,89;,
   3;90,91,92;,
   3;93,94,95;,
   3;96,97,98;,
   3;99,100,101;,
   3;102,103,104;,
   3;105,106,107;,
   3;108,109,110;,
   3;111,112,113;,
   3;114,115,116;,
   3;117,118,119;,
   3;120,121,122;,
   3;123,124,125;,
   3;126,127,128;,
   3;129,130,131;,
   3;132,133,134;,
   3;135,136,137;,
   3;138,139,140;,
   3;141,142,143;,
   3;144,145,146;,
   3;147,148,149;,
   3;150,151,152;,
   3;153,154,155;,
   3;156,157,158;,
   3;159,160,161;,
   3;162,163,164;,
   3;165,166,167;,
   3;168,169,170;,
   3;171,172,173;,
   3;174,175,176;,
   3;177,178,179;,
   3;180,181,182;,
   3;183,184,185;,
   3;186,187,188;,
   3;189,190,191;,
   3;192,193,194;,
   3;195,196,197;,
   3;198,199,200;,
   3;201,202,203;;

   MeshNormals  {
    204;
    0.296204;0.015006;0.955007;,
    -0.313921;0.014900;0.949332;,
    -0.390897;-0.447050;0.804578;,
    -0.390897;-0.447050;0.804578;,
    0.387961;-0.436492;0.811764;,
    0.296204;0.015006;0.955007;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    0.403226;0.446410;0.798828;,
    -0.406481;0.457572;0.790823;,
    -0.313921;0.014900;0.949332;,
    -0.313921;0.014900;0.949332;,
    0.296204;0.015006;0.955007;,
    0.403226;0.446410;0.798828;,
    -0.406481;0.457572;0.790823;,
    -0.830220;-0.000000;0.557436;,
    -0.313921;0.014900;0.949332;,
    -0.313921;0.014900;0.949332;,
    -0.830220;-0.000000;0.557436;,
    -0.390897;-0.447050;0.804578;,
    0.387961;-0.436492;0.811764;,
    0.808443;0.000000;0.588575;,
    0.296204;0.015006;0.955007;,
    0.296204;0.015006;0.955007;,
    0.808443;0.000000;0.588575;,
    0.403226;0.446410;0.798828;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000001;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    0.301511;0.904534;-0.301511;,
    0.707107;0.707107;0.000000;,
    -0.000000;1.000000;0.000000;,
    0.301511;0.904534;-0.301511;,
    -0.000000;1.000000;0.000000;,
    -0.000000;1.000000;0.000000;,
    -0.301511;0.904534;-0.301511;,
    0.301511;0.904534;-0.301511;,
    -0.000000;1.000000;0.000000;,
    -0.301511;0.904534;-0.301511;,
    -0.000000;1.000000;0.000000;,
    0.000000;1.000000;0.000000;,
    -0.301511;0.904534;-0.301511;,
    0.000000;1.000000;0.000000;,
    0.000000;1.000000;-0.000000;,
    -0.301511;0.904534;-0.301511;,
    0.000000;1.000000;-0.000000;,
    -0.707107;0.707107;0.000000;,
    0.000158;-1.000000;0.000158;,
    0.000413;-1.000000;0.000000;,
    0.000413;-1.000000;0.000000;,
    0.000158;-1.000000;0.000158;,
    0.000413;-1.000000;0.000000;,
    0.000227;-1.000000;0.000218;,
    0.000158;-1.000000;0.000158;,
    0.000227;-1.000000;0.000218;,
    -0.000085;-1.000000;0.000034;,
    -0.000325;-1.000000;-0.000133;,
    0.000158;-1.000000;0.000158;,
    -0.000085;-1.000000;0.000034;,
    -0.000325;-1.000000;-0.000133;,
    -0.000085;-1.000000;0.000034;,
    -0.000156;-1.000000;0.000239;,
    -0.000325;-1.000000;-0.000133;,
    -0.000156;-1.000000;0.000239;,
    -0.000423;-1.000000;0.000647;,
    0.707064;0.000000;-0.707149;,
    1.000000;0.000000;0.000000;,
    0.707107;0.707107;0.000000;,
    0.707107;0.707107;0.000000;,
    0.301511;0.904534;-0.301511;,
    0.707064;0.000000;-0.707149;,
    -0.707003;-0.000000;-0.707210;,
    0.707064;0.000000;-0.707149;,
    0.301511;0.904534;-0.301511;,
    0.301511;0.904534;-0.301511;,
    -0.301511;0.904534;-0.301511;,
    -0.707003;-0.000000;-0.707210;,
    -1.000000;-0.000000;-0.000000;,
    -0.707003;-0.000000;-0.707210;,
    -0.301511;0.904534;-0.301511;,
    -0.301511;0.904534;-0.301511;,
    -0.707107;0.707107;0.000000;,
    -1.000000;-0.000000;-0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    -1.000000;-0.000000;-0.000000;,
    -1.000000;-0.000000;-0.000000;,
    -1.000000;-0.000000;-0.000000;,
    -1.000000;-0.000000;-0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;-0.000000;,
    0.999997;0.001454;0.001843;,
    1.000000;-0.000000;0.000000;,
    0.999995;0.001970;0.002497;,
    0.999995;0.001970;0.002497;,
    0.999985;0.003419;0.004333;,
    0.999997;0.001454;0.001843;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    -0.000014;1.000000;0.000820;,
    -0.000072;1.000000;0.000919;,
    0.000042;1.000000;0.000722;,
    0.000042;1.000000;0.000722;,
    0.000100;1.000000;0.000623;,
    -0.000014;1.000000;0.000820;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    -0.000001;1.000000;0.000000;,
    -0.000001;1.000000;0.000000;,
    -0.000001;1.000000;0.000000;,
    -0.000001;1.000000;0.000000;,
    -0.000001;1.000000;0.000000;,
    -0.000001;1.000000;0.000000;,
    -1.000000;0.000000;0.000000;,
    -1.000000;0.000000;0.000000;,
    -1.000000;0.000000;0.000000;,
    -1.000000;0.000000;0.000000;,
    -1.000000;0.000000;0.000000;,
    -1.000000;0.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    -0.000001;-1.000000;0.000000;,
    -0.000001;-1.000000;0.000000;,
    -0.000001;-1.000000;0.000000;,
    -0.000001;-1.000000;0.000000;,
    -0.000001;-1.000000;0.000000;,
    -0.000001;-1.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    -1.000000;0.000000;0.000000;,
    -1.000000;-0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    1.000000;0.000000;0.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    1.000000;0.000006;0.000000;,
    1.000000;0.000007;0.000000;,
    1.000000;0.000000;0.000001;,
    1.000000;0.000000;0.000001;,
    1.000000;0.000000;0.000001;,
    1.000000;0.000006;0.000000;,
    -1.000000;-0.000005;-0.000001;,
    -1.000000;-0.000005;-0.000001;,
    -1.000000;-0.000000;-0.000000;,
    -1.000000;-0.000000;-0.000000;,
    -1.000000;0.000000;0.000000;,
    -1.000000;-0.000005;-0.000001;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-1.000000;0.000000;,
    0.000000;-0.492533;0.870294;,
    0.000000;-0.492533;0.870294;,
    0.000000;-0.492533;0.870294;,
    0.000000;-0.492533;0.870294;,
    0.000000;-0.492533;0.870294;,
    0.000000;-0.492533;0.870294;;
    68;
    3;0,1,2;,
    3;3,4,5;,
    3;6,7,8;,
    3;9,10,11;,
    3;12,13,14;,
    3;15,16,17;,
    3;18,19,20;,
    3;21,22,23;,
    3;24,25,26;,
    3;27,28,29;,
    3;30,31,32;,
    3;33,34,35;,
    3;36,37,38;,
    3;39,40,41;,
    3;42,43,44;,
    3;45,46,47;,
    3;48,49,50;,
    3;51,52,53;,
    3;54,55,56;,
    3;57,58,59;,
    3;60,61,62;,
    3;63,64,65;,
    3;66,67,68;,
    3;69,70,71;,
    3;72,73,74;,
    3;75,76,77;,
    3;78,79,80;,
    3;81,82,83;,
    3;84,85,86;,
    3;87,88,89;,
    3;90,91,92;,
    3;93,94,95;,
    3;96,97,98;,
    3;99,100,101;,
    3;102,103,104;,
    3;105,106,107;,
    3;108,109,110;,
    3;111,112,113;,
    3;114,115,116;,
    3;117,118,119;,
    3;120,121,122;,
    3;123,124,125;,
    3;126,127,128;,
    3;129,130,131;,
    3;132,133,134;,
    3;135,136,137;,
    3;138,139,140;,
    3;141,142,143;,
    3;144,145,146;,
    3;147,148,149;,
    3;150,151,152;,
    3;153,154,155;,
    3;156,157,158;,
    3;159,160,161;,
    3;162,163,164;,
    3;165,166,167;,
    3;168,169,170;,
    3;171,172,173;,
    3;174,175,176;,
    3;177,178,179;,
    3;180,181,182;,
    3;183,184,185;,
    3;186,187,188;,
    3;189,190,191;,
    3;192,193,194;,
    3;195,196,197;,
    3;198,199,200;,
    3;201,202,203;;
   }

   MeshMaterialList  {
    1;
    68;
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0;
    { buildz5 }
   }

   MeshTextureCoords  {
    204;
    0.494432;0.443138;,
    0.492968;0.665423;,
    0.547338;0.693830;,
    0.547338;0.693830;,
    0.547337;0.412410;,
    0.494432;0.443138;,
    0.104799;0.696053;,
    0.022088;0.696053;,
    0.022088;0.997074;,
    0.104799;0.696053;,
    0.022088;0.997074;,
    0.209163;0.997074;,
    0.104799;0.696053;,
    0.209163;0.997074;,
    0.209163;0.696053;,
    0.546701;0.412410;,
    0.546703;0.693830;,
    0.492968;0.665423;,
    0.492968;0.665423;,
    0.494432;0.443138;,
    0.546701;0.412410;,
    0.545568;0.624344;,
    0.545568;0.572744;,
    0.494547;0.571782;,
    0.494547;0.571782;,
    0.545568;0.572744;,
    0.545567;0.507636;,
    0.545901;0.628762;,
    0.545901;0.573178;,
    0.494483;0.572138;,
    0.494483;0.572138;,
    0.545901;0.573178;,
    0.545901;0.512709;,
    0.097762;0.997074;,
    0.101453;0.696053;,
    0.184164;0.696053;,
    0.184164;0.696053;,
    0.295911;0.696053;,
    0.292219;0.997074;,
    0.097762;0.997074;,
    0.184164;0.696053;,
    0.292219;0.997074;,
    0.363088;0.868482;,
    0.363088;0.997074;,
    0.546926;0.997074;,
    0.363088;0.868482;,
    0.546926;0.997074;,
    0.546926;0.696053;,
    0.193675;0.868482;,
    0.363088;0.868482;,
    0.546926;0.696053;,
    0.193675;0.868482;,
    0.546926;0.696053;,
    0.005448;0.696053;,
    0.193675;0.868482;,
    0.005448;0.696053;,
    0.005448;0.997074;,
    0.193675;0.868482;,
    0.005448;0.997074;,
    0.193675;0.997074;,
    0.358699;0.868482;,
    0.358699;0.997074;,
    0.546926;0.997074;,
    0.358699;0.868482;,
    0.546926;0.997074;,
    0.546926;0.696053;,
    0.358699;0.868482;,
    0.546926;0.696053;,
    0.005448;0.696053;,
    0.189286;0.868482;,
    0.358699;0.868482;,
    0.005448;0.696053;,
    0.189286;0.868482;,
    0.005448;0.696053;,
    0.005448;0.997074;,
    0.189286;0.868482;,
    0.005448;0.997074;,
    0.189286;0.997074;,
    0.742570;0.570546;,
    0.742570;0.710991;,
    0.585026;0.710991;,
    0.585026;0.710991;,
    0.585026;0.570546;,
    0.742570;0.570546;,
    0.157483;0.849247;,
    0.157483;0.989691;,
    -0.000060;0.989691;,
    -0.000060;0.989691;,
    -0.000060;0.849247;,
    0.157483;0.849247;,
    0.585026;0.710991;,
    0.585026;0.570546;,
    0.742570;0.570546;,
    0.742570;0.570546;,
    0.742570;0.710991;,
    0.585026;0.710991;,
    0.616403;0.710991;,
    0.616403;0.570546;,
    0.855158;0.570546;,
    0.855158;0.570546;,
    0.855158;0.710991;,
    0.616403;0.710991;,
    0.989894;0.570546;,
    0.989894;0.710991;,
    0.791745;0.710991;,
    0.791745;0.710991;,
    0.791745;0.570546;,
    0.989894;0.570546;,
    0.742570;0.570546;,
    0.742570;0.710991;,
    0.585026;0.710991;,
    0.585026;0.710991;,
    0.585026;0.570546;,
    0.742570;0.570546;,
    0.105842;0.877924;,
    0.105842;1.002474;,
    0.004227;1.002474;,
    0.004227;1.002474;,
    0.004121;0.877924;,
    0.105842;0.877924;,
    0.742570;0.570546;,
    0.742570;0.710991;,
    0.585026;0.710991;,
    0.585026;0.710991;,
    0.585026;0.570546;,
    0.742570;0.570546;,
    0.546267;0.709145;,
    0.742570;0.709145;,
    0.742570;0.574237;,
    0.546267;0.709145;,
    0.742570;0.574237;,
    0.544924;0.574237;,
    0.349436;0.828944;,
    0.097733;0.828944;,
    0.097733;0.963851;,
    0.097733;0.963851;,
    0.349436;0.963851;,
    0.349436;0.828944;,
    0.452136;0.627934;,
    0.725959;0.627934;,
    0.725959;0.493026;,
    0.452136;0.627934;,
    0.725959;0.493026;,
    0.451791;0.493026;,
    0.831164;0.710819;,
    0.548083;0.710819;,
    0.548083;0.816195;,
    0.548083;0.816195;,
    0.831164;0.816195;,
    0.831164;0.710819;,
    0.537473;0.748327;,
    0.472284;0.747679;,
    0.471362;0.874742;,
    0.471362;0.874742;,
    0.537474;0.876049;,
    0.537473;0.748327;,
    0.183252;0.876406;,
    0.182216;0.746584;,
    0.319152;0.746584;,
    0.183252;0.876406;,
    0.319152;0.746584;,
    0.319152;0.876406;,
    0.258267;0.983457;,
    0.258267;0.853635;,
    0.026125;0.853635;,
    0.026125;0.853635;,
    0.027329;0.983457;,
    0.258267;0.983457;,
    0.259893;0.985303;,
    0.260361;0.855481;,
    0.027560;0.855481;,
    0.027560;0.855481;,
    0.027560;0.985303;,
    0.259893;0.985303;,
    0.471362;0.874742;,
    0.537474;0.876049;,
    0.537473;0.748327;,
    0.537473;0.748327;,
    0.472284;0.747679;,
    0.471362;0.874742;,
    0.464867;0.698005;,
    0.429856;0.698004;,
    0.429855;0.894625;,
    0.429855;0.894625;,
    0.464867;0.894532;,
    0.464867;0.698005;,
    0.463014;0.698006;,
    0.428002;0.698006;,
    0.428002;0.894809;,
    0.428002;0.894809;,
    0.463014;0.894809;,
    0.463014;0.698006;,
    0.461954;0.698006;,
    0.432319;0.698006;,
    0.432320;0.894795;,
    0.432320;0.894795;,
    0.461954;0.894795;,
    0.461954;0.698006;,
    0.464867;0.771219;,
    0.429856;0.771219;,
    0.429856;0.931211;,
    0.429856;0.931211;,
    0.464867;0.931210;,
    0.464867;0.771219;;
   }
  }
 }
}