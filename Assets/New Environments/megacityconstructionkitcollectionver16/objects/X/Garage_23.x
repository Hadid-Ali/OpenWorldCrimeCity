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

Frame Garage_23 {
 

 FrameTransformMatrix {
  1.000000,0.000000,0.000000,0.000000,0.000000,-0.000000,-1.000000,0.000000,0.000000,1.000000,-0.000000,0.000000,0.000000,-12.670539,12.491668,1.000000;;
 }

 Frame {
  

  FrameTransformMatrix {
   1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,-0.000000,0.000000,1.000000;;
  }

  Mesh  {
   66;
   24.706671;14.114822;6.683790;,
   24.706671;14.114824;-0.000000;,
   -24.649513;14.104387;-0.000000;,
   -24.649513;14.104387;-0.000000;,
   -24.649511;14.104385;6.683790;,
   24.706671;14.114822;6.683790;,
   -14.990926;4.423944;6.683790;,
   -24.649511;14.104385;6.683790;,
   -24.706661;-14.114763;6.683790;,
   -24.706661;-14.114763;6.683790;,
   -15.016210;-14.114762;6.683790;,
   -14.990926;4.423944;6.683790;,
   -15.016210;-14.114762;6.683790;,
   -24.706661;-14.114763;6.683790;,
   -24.706661;-14.114771;-0.000000;,
   -24.706661;-14.114771;-0.000000;,
   -15.016210;-14.114768;-0.000000;,
   -15.016210;-14.114762;6.683790;,
   -15.016210;-14.114762;6.683790;,
   -15.016210;-14.114768;-0.000000;,
   -14.990926;4.423944;-0.000000;,
   -14.990926;4.423944;-0.000000;,
   -14.990926;4.423944;6.683790;,
   -15.016210;-14.114762;6.683790;,
   -24.649511;14.104385;6.683790;,
   -24.649513;14.104387;-0.000000;,
   -24.706661;-14.114771;-0.000000;,
   -24.706661;-14.114771;-0.000000;,
   -24.706661;-14.114763;6.683790;,
   -24.649511;14.104385;6.683790;,
   24.703156;-14.114772;6.683791;,
   24.706671;14.114822;6.683790;,
   15.009453;4.423336;6.683790;,
   15.012702;-14.114765;6.683791;,
   24.703156;-14.114772;6.683791;,
   15.009453;4.423336;6.683790;,
   24.703156;-14.114773;-0.000000;,
   24.703156;-14.114772;6.683791;,
   15.012702;-14.114765;6.683791;,
   15.012702;-14.114765;6.683791;,
   15.012702;-14.114771;-0.000000;,
   24.703156;-14.114773;-0.000000;,
   24.703156;-14.114773;-0.000000;,
   24.706671;14.114824;-0.000000;,
   24.706671;14.114822;6.683790;,
   24.706671;14.114822;6.683790;,
   24.703156;-14.114772;6.683791;,
   24.703156;-14.114773;-0.000000;,
   -24.649511;14.104385;6.683790;,
   -14.990926;4.423944;6.683790;,
   15.009453;4.423336;6.683790;,
   15.009453;4.423336;6.683790;,
   24.706671;14.114822;6.683790;,
   -24.649511;14.104385;6.683790;,
   -14.990926;4.423944;6.683790;,
   -14.990926;4.423944;-0.000000;,
   15.009453;4.423335;-0.000000;,
   15.009453;4.423335;-0.000000;,
   15.009453;4.423336;6.683790;,
   -14.990926;4.423944;6.683790;,
   15.012702;-14.114771;-0.000000;,
   15.012702;-14.114765;6.683791;,
   15.009453;4.423336;6.683790;,
   15.009453;4.423336;6.683790;,
   15.009453;4.423335;-0.000000;,
   15.012702;-14.114771;-0.000000;;
   22;
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
   3;63,64,65;;

   MeshNormals  {
    66;
    -0.000211;1.000000;0.000000;,
    -0.000211;1.000000;0.000000;,
    -0.000211;1.000000;0.000000;,
    -0.000211;1.000000;0.000000;,
    -0.000211;1.000000;0.000000;,
    -0.000211;1.000000;0.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;-1.000000;0.000001;,
    0.000000;-1.000000;0.000001;,
    0.000000;-1.000000;0.000001;,
    0.000000;-1.000000;0.000001;,
    0.000000;-1.000000;0.000001;,
    0.000000;-1.000000;0.000001;,
    0.999999;-0.001364;0.000000;,
    0.999999;-0.001364;0.000000;,
    0.999999;-0.001364;0.000000;,
    0.999999;-0.001364;0.000000;,
    0.999999;-0.001364;0.000000;,
    0.999999;-0.001364;0.000000;,
    -0.999998;0.002025;0.000000;,
    -0.999998;0.002025;0.000000;,
    -0.999998;0.002025;0.000000;,
    -0.999998;0.002025;0.000000;,
    -0.999998;0.002025;-0.000000;,
    -0.999998;0.002025;0.000000;,
    -0.000000;0.000000;1.000000;,
    -0.000000;0.000000;1.000000;,
    -0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    -0.000000;0.000000;1.000000;,
    -0.000000;0.000000;1.000000;,
    -0.000000;-1.000000;0.000000;,
    -0.000001;-1.000000;0.000000;,
    -0.000000;-1.000000;0.000001;,
    -0.000000;-1.000000;0.000001;,
    -0.000000;-1.000000;0.000001;,
    -0.000000;-1.000000;0.000000;,
    1.000000;-0.000125;0.000000;,
    1.000000;-0.000125;-0.000000;,
    1.000000;-0.000125;-0.000000;,
    1.000000;-0.000125;-0.000000;,
    1.000000;-0.000125;0.000000;,
    1.000000;-0.000125;0.000000;,
    0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    -0.000000;0.000000;1.000000;,
    -0.000000;0.000000;1.000000;,
    -0.000000;0.000000;1.000000;,
    0.000000;0.000000;1.000000;,
    -0.000020;-1.000000;0.000000;,
    -0.000020;-1.000000;0.000000;,
    -0.000020;-1.000000;0.000000;,
    -0.000020;-1.000000;0.000000;,
    -0.000020;-1.000000;0.000000;,
    -0.000020;-1.000000;0.000000;,
    -1.000000;-0.000175;0.000000;,
    -1.000000;-0.000175;0.000000;,
    -1.000000;-0.000175;0.000000;,
    -1.000000;-0.000175;0.000000;,
    -1.000000;-0.000175;0.000000;,
    -1.000000;-0.000175;0.000000;;
    22;
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
    3;63,64,65;;
   }

   MeshMaterialList  {
    1;
    22;
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
    { buildz6 }
   }

   MeshTextureCoords  {
    66;
    0.004315;0.133734;,
    0.004315;0.265918;,
    0.997490;0.265919;,
    0.997490;0.265919;,
    0.997490;0.133734;,
    0.004315;0.133734;,
    0.855565;0.433008;,
    0.921926;0.387215;,
    0.728694;0.387311;,
    0.728694;0.387311;,
    0.728694;0.433081;,
    0.855565;0.433008;,
    0.693043;0.133734;,
    0.474595;0.134280;,
    0.474595;0.266466;,
    0.474595;0.266466;,
    0.693043;0.265919;,
    0.693043;0.133734;,
    0.903366;0.133734;,
    0.903366;0.265919;,
    0.508712;0.265919;,
    0.508712;0.265919;,
    0.508712;0.133734;,
    0.903366;0.133734;,
    0.003274;0.133734;,
    0.003274;0.265918;,
    0.604356;0.265919;,
    0.604356;0.265919;,
    0.604356;0.133734;,
    0.003274;0.133734;,
    0.728694;0.387311;,
    0.921926;0.387215;,
    0.855523;0.433008;,
    0.728694;0.433081;,
    0.728694;0.387311;,
    0.855523;0.433008;,
    0.474595;0.266466;,
    0.474595;0.134280;,
    0.693043;0.133734;,
    0.693043;0.133734;,
    0.693043;0.265919;,
    0.474595;0.266466;,
    0.604356;0.265919;,
    0.003274;0.265918;,
    0.003274;0.133734;,
    0.003274;0.133734;,
    0.604356;0.133734;,
    0.604356;0.265919;,
    0.997691;0.432970;,
    0.945231;0.387234;,
    0.781609;0.387292;,
    0.781609;0.387292;,
    0.729052;0.433081;,
    0.997691;0.432970;,
    -0.002396;0.133734;,
    -0.002396;0.265919;,
    0.593295;0.265919;,
    0.593295;0.265919;,
    0.593295;0.133734;,
    -0.002396;0.133734;,
    0.903237;0.265919;,
    0.903237;0.133734;,
    0.508712;0.133734;,
    0.508712;0.133734;,
    0.508712;0.265919;,
    0.903237;0.265919;;
   }
  }
 }
}