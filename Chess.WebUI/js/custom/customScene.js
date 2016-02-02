var CustomScene = function (canvas, engine, functionApplyAfterLoadMesh) {
	this._canvas = canvas;
	this._engine = engine;
	this._scene = this.createScene(functionApplyAfterLoadMesh);
	this._centerPoints = {
		//a
		A1: new BABYLON.Vector3(8.8, 2.5, -8.75),
		A2: new BABYLON.Vector3(6.3, 2.5, -8.75),
		A3: new BABYLON.Vector3(3.8, 2.5, -8.75),
		A4: new BABYLON.Vector3(1.3, 2.5, -8.75),
		A5: new BABYLON.Vector3(-1.2, 2.5, -8.75),
		A6: new BABYLON.Vector3(-3.7, 2.5, -8.75),
		A7: new BABYLON.Vector3(-6.2, 2.5, -8.75),
		A8: new BABYLON.Vector3(-8.7, 2.5, -8.75),
		//b
		B1: new BABYLON.Vector3(8.8, 2.5, -6.25),
		B2: new BABYLON.Vector3(6.3, 2.5, -6.25),
		B3: new BABYLON.Vector3(3.8, 2.5, -6.25),
		B4: new BABYLON.Vector3(1.3, 2.5, -6.25),
		B5: new BABYLON.Vector3(-1.2, 2.5, -6.25),
		B6: new BABYLON.Vector3(-3.7, 2.5, -6.25),
		B7: new BABYLON.Vector3(-6.2, 2.5, -6.25),
		B8: new BABYLON.Vector3(-8.7, 2.5, -6.25),
		//c
		C1: new BABYLON.Vector3(8.8, 2.5, -3.75),
		C2: new BABYLON.Vector3(6.3, 2.5, -3.75),
		C3: new BABYLON.Vector3(3.8, 2.5, -3.75),
		C4: new BABYLON.Vector3(1.3, 2.5, -3.75),
		C5: new BABYLON.Vector3(-1.2, 2.5, -3.75),
		C6: new BABYLON.Vector3(-3.7, 2.5, -3.75),
		C7: new BABYLON.Vector3(-6.2, 2.5, -3.75),
		C8: new BABYLON.Vector3(-8.7, 2.5, -3.75),
		//d
		D1: new BABYLON.Vector3(8.8, 2.5, -1.25),
		D2: new BABYLON.Vector3(6.3, 2.5, -1.25),
		D3: new BABYLON.Vector3(3.8, 2.5, -1.25),
		D4: new BABYLON.Vector3(1.3, 2.5, -1.25),
		D5: new BABYLON.Vector3(-1.2, 2.5, -1.25),
		D6: new BABYLON.Vector3(-3.7, 2.5, -1.25),
		D7: new BABYLON.Vector3(-6.2, 2.5, -1.25),
		D8: new BABYLON.Vector3(-8.7, 2.5, -1.25),
		//e
		E1: new BABYLON.Vector3(8.8, 2.5, 1.25),
		E2: new BABYLON.Vector3(6.3, 2.5, 1.25),
		E3: new BABYLON.Vector3(3.8, 2.5, 1.25),
		E4: new BABYLON.Vector3(1.3, 2.5, 1.25),
		E5: new BABYLON.Vector3(-1.2, 2.5, 1.25),
		E6: new BABYLON.Vector3(-3.7, 2.5, 1.25),
		E7: new BABYLON.Vector3(-6.2, 2.5, 1.25),
		E8: new BABYLON.Vector3(-8.7, 2.5, 1.25),
		//f
		F1: new BABYLON.Vector3(8.8, 2.5, 3.75),
		F2: new BABYLON.Vector3(6.3, 2.5, 3.75),
		F3: new BABYLON.Vector3(3.8, 2.5, 3.75),
		F4: new BABYLON.Vector3(1.3, 2.5, 3.75),
		F5: new BABYLON.Vector3(-1.2, 2.5, 3.75),
		F6: new BABYLON.Vector3(-3.7, 2.5, 3.75),
		F7: new BABYLON.Vector3(-6.2, 2.5, 3.75),
		F8: new BABYLON.Vector3(-8.7, 2.5, 3.75),
		//g
		G1: new BABYLON.Vector3(8.8, 2.5, 6.25),
		G2: new BABYLON.Vector3(6.3, 2.5, 6.25),
		G3: new BABYLON.Vector3(3.8, 2.5, 6.25),
		G4: new BABYLON.Vector3(1.3, 2.5, 6.25),
		G5: new BABYLON.Vector3(-1.2, 2.5, 6.25),
		G6: new BABYLON.Vector3(-3.7, 2.5, 6.25),
		G7: new BABYLON.Vector3(-6.2, 2.5, 6.25),
		G8: new BABYLON.Vector3(-8.7, 2.5, 6.25),
		//h
		H1: new BABYLON.Vector3(8.8, 2.5, 8.75),
		H2: new BABYLON.Vector3(6.3, 2.5, 8.75),
		H3: new BABYLON.Vector3(3.8, 2.5, 8.75),
		H4: new BABYLON.Vector3(1.3, 2.5, 8.75),
		H5: new BABYLON.Vector3(-1.2, 2.5, 8.75),
		H6: new BABYLON.Vector3(-3.7, 2.5, 8.75),
		H7: new BABYLON.Vector3(-6.2, 2.5, 8.75),
		H8: new BABYLON.Vector3(-8.7, 2.5, 8.75),
	};
};

CustomScene.prototype.roundPoint = function (currentPoint) {
	var result = undefined;
	for (key in this._centerPoints) {
		var point = this._centerPoints[key];
		if (currentPoint.x < point.x + 1.25 && currentPoint.x > point.x - 1.25
            && currentPoint.z < point.z + 1.25 && currentPoint.z > point.z - 1.25) {
			result = this._centerPoints[key];
		}
	};
	return result;
};



CustomScene.prototype.createScene = function (functionApplyAfterLoadMesh) {
	// This creates a basic Babylon Scene object (non-mesh)
	var scene = new BABYLON.Scene(this._engine);

	scene.clearColor = new BABYLON.Color3(0, 0, 0);
	scene.shadowsEnabled = true;

	// This creates and positions a free camera (non-mesh)
	var camera = new BABYLON.ArcRotateCamera("Camera", 0, 0.8, 40, BABYLON.Vector3.Zero(), scene);
	camera.lowerBetaLimit = 0.1;
	camera.upperBetaLimit = (Math.PI / 2) * 0.9;
	camera.lowerRadiusLimit = 30;
	camera.upperRadiusLimit = 30;
	camera.attachControl(this._canvas, true);

	var light = new BABYLON.PointLight("Omni0", new BABYLON.Vector3(-17.6, 18.8, -49.9), scene);
	light.intensity = 2.5;

	// Our built-in 'ground' shape. Params: name, width, depth, subdivs, scene
	var plane = BABYLON.Mesh.CreatePlane("plane", 20, scene);
	plane.position.y = 0;
	plane.rotation.x = Math.PI / 2;


	var materialPlane = new BABYLON.StandardMaterial("texturePlane", scene);
	materialPlane.diffuseTexture = new BABYLON.Texture("textures/chessboard_texture_by_sveinjo.png", scene);
	materialPlane.diffuseTexture.uScale = 2.0;//Repeat 5 times on the Vertical Axes
	materialPlane.diffuseTexture.vScale = 2.0;//Repeat 5 times on the Horizontal Axes
	materialPlane.backFaceCulling = false;//Allways show the front and the back of an element
	plane.material = materialPlane;




	//scene.onPointerDown = function (evt, pickResult) {
	//    // if the click hits the ground object, we change the impact position
	//    if (pickResult.hit) {
	//        var roudingPoint = roundPoint(pickResult.pickedPoint);
	//        scene.meshes[6].position.x = roudingPoint.x;
	//        scene.meshes[6].position.z = roudingPoint.z;
	//    }
	//};


	var skybox = BABYLON.Mesh.CreateBox("skyBox", 100.0, scene);
	var skyboxMaterial = new BABYLON.StandardMaterial("skyBox", scene);
	skyboxMaterial.backFaceCulling = false;
	skyboxMaterial.reflectionTexture = new BABYLON.CubeTexture("textures/TropicalSunnyDay", scene);
	skyboxMaterial.reflectionTexture.coordinatesMode = BABYLON.Texture.SKYBOX_MODE;
	skyboxMaterial.diffuseColor = new BABYLON.Color3(0, 0, 0);
	skyboxMaterial.specularColor = new BABYLON.Color3(0, 0, 0);
	skyboxMaterial.disableLighting = true;
	skybox.material = skyboxMaterial;

	//white material
	var whiteFigureMaterial = new BABYLON.StandardMaterial("kosh", scene);
	whiteFigureMaterial.reflectionTexture = new BABYLON.CubeTexture("textures/TropicalSunnyDay", scene);
	whiteFigureMaterial.diffuseColor = new BABYLON.Color3(0, 0, 0);
	whiteFigureMaterial.emissiveColor = new BABYLON.Color3(0.5, 0.5, 0.5);
	whiteFigureMaterial.alpha = 0.4;
	whiteFigureMaterial.specularPower = 16;

	whiteFigureMaterial.reflectionFresnelParameters = new BABYLON.FresnelParameters();
	whiteFigureMaterial.reflectionFresnelParameters.bias = 0.1;

	whiteFigureMaterial.emissiveFresnelParameters = new BABYLON.FresnelParameters();
	whiteFigureMaterial.emissiveFresnelParameters.bias = 0.6;
	whiteFigureMaterial.emissiveFresnelParameters.power = 4;
	whiteFigureMaterial.emissiveFresnelParameters.leftColor = BABYLON.Color3.White();
	whiteFigureMaterial.emissiveFresnelParameters.rightColor = BABYLON.Color3.White();

	whiteFigureMaterial.opacityFresnelParameters = new BABYLON.FresnelParameters();
	whiteFigureMaterial.opacityFresnelParameters.leftColor = BABYLON.Color3.White();
	whiteFigureMaterial.opacityFresnelParameters.rightColor = BABYLON.Color3.Black();

	//black
	var blackFigureMaterial = new BABYLON.StandardMaterial("kosh", scene);
	blackFigureMaterial.reflectionTexture = new BABYLON.CubeTexture("textures/TropicalSunnyDay", scene);
	blackFigureMaterial.diffuseColor = new BABYLON.Color3(0, 0, 0);
	blackFigureMaterial.emissiveColor = new BABYLON.Color3(0.5, 0.5, 0.5);
	blackFigureMaterial.alpha = 0.4;
	blackFigureMaterial.specularPower = 16;

	blackFigureMaterial.reflectionFresnelParameters = new BABYLON.FresnelParameters();
	blackFigureMaterial.reflectionFresnelParameters.bias = 100;

	blackFigureMaterial.emissiveFresnelParameters = new BABYLON.FresnelParameters();
	blackFigureMaterial.emissiveFresnelParameters.bias = 0.6;
	blackFigureMaterial.emissiveFresnelParameters.power = 4;
	blackFigureMaterial.emissiveFresnelParameters.leftColor = BABYLON.Color3.Black();
	blackFigureMaterial.emissiveFresnelParameters.rightColor = BABYLON.Color3.Black();

	blackFigureMaterial.opacityFresnelParameters = new BABYLON.FresnelParameters();
	blackFigureMaterial.opacityFresnelParameters.leftColor = BABYLON.Color3.White();
	blackFigureMaterial.opacityFresnelParameters.rightColor = BABYLON.Color3.Black();

	BABYLON.SceneLoader.ImportMesh("", "textures/figures/", "figures.babylon", scene, function () {
		for (var i = 2; i < scene.meshes.length; i++) {
			scene.meshes[i].isVisible = false;
			scene.meshes[i].isUse = false;
			if (scene.meshes[i].name.indexOf("black") > -1) {
				scene.meshes[i].rotation.y = -Math.PI / 2;
				scene.meshes[i].material = blackFigureMaterial;
			}
			if (scene.meshes[i].name.indexOf("white") > -1) {
				scene.meshes[i].rotation.y = Math.PI / 2;
				scene.meshes[i].material = whiteFigureMaterial;
			}
		}
	
		//// Shadows
		plane.receiveShadows = true;
		functionApplyAfterLoadMesh();
	});
	
	return scene;
};

CustomScene.prototype.getScene = function () {
	
	return this._scene;
};

CustomScene.prototype.getChessPositionByPosition = function (pickPosition) {
	var roundPosition = this.roundPoint(pickPosition);
	var res = undefined;
	for (var prop in this._centerPoints) {
		if (this._centerPoints.hasOwnProperty(prop)) {
			if (this._centerPoints[prop].x === roundPosition.x && this._centerPoints[prop].z === roundPosition.z)
				res = {X: prop[0], Y: prop[1]};
		}
	}
	return res;
}

CustomScene.prototype.getMeshIdByName = function (name) {
	var result = -1;
	angular.forEach(this._scene.meshes, function (value, key) {
		if (value.name == name) result = key;
	});
	return result;
};

CustomScene.prototype.getMeshIdByPosition = function (position) {
	var result = -1;
	angular.forEach(this._scene.meshes, function(value, key) {
		if (value.position.x === position.x && value.position.y === position.y && value.position.z === position.z && value.isVisible) result = key;
	});
	return result;
}

CustomScene.prototype.selectMeshById = function (meshId) {
	console.log(this._scene.meshes[meshId]);
	this._scene.meshes[meshId].isSelected = true;
	this._scene.meshes[meshId].position.y += 1;
}


CustomScene.prototype.refreshSelectingMesh = function () {
	angular.forEach(this._scene.meshes, function (value, key) {
		if (value.isSelected) {
			value.position.y -= 1;
			value.isSelected = false;
		}
	});
}

CustomScene.prototype.getActiveFigureKey = function () {
	var result = -1;
	angular.forEach(this._scene.meshes, function (value, key) {
		if (value.isSelected) result = key;
	});
	return result;
}

CustomScene.prototype.getCenterPointsByPosition = function (position) {
	var key = position.X + position.Y;
	return this._centerPoints[key];
};

CustomScene.prototype.refreshTempProperties = function () {
	for (var i = 2; i < this._scene.meshes.length; i++) {
			this._scene.meshes[i].isUse = false;
			this._scene.meshes[i].isVisible = false;
	};

};

CustomScene.prototype.renderScene = function () {
	this._engine.runRenderLoop(function () {
	
		this._scene.render();
	});
}