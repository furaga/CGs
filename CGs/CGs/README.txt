1. 簡易レイトレーシング

	[ソースコード]
	・GameRatrace.cs
	・Renderer.cs
	・Raytraceディレクトリ以下

	[概要]
	・http://www.not-enough.org/abe/manual/ray-project/ をC#（XNA）に移植したもの
	・非常に簡略化されたレイトレースプログラム
	・意味もなく毎フレーム計算しなおしている
	・さすがに処理落ちするね

2. レイトレーシングCSV（ちゃんとした版）

	[ソースコード]
	・GameRaytrace.cs
	・Renderer.cs
	・Raytrace2ディレクトリ以下

	[概要]
	・もう少しまともなレイトレ
	・http://www.t-pot.com/program/index.html を参考に実装
	・反射・屈折・影付けを実装
	・図形は球・三角形・四角形・チェックの入った平面を用意。
	
3. HLSLその1

	[ソースコード]
	・GameHLSL.cs
	・Content/Shader/sample.fx

	[概要]
	・HLSLの練習プログラムその１
	・立方体が回るだけ

3. HLSLその2

	[ソースコード]
	・GameHLSL2.cs
	・Content/Shader/sample.fx
	・Content/Shader/sample2.fx

	[概要]
	・HLSLの練習プログラムその2
	・回転する立方体が描かれたスクリーンが回転する
	・RenderTarget2D（FBOのラッパー？）
