<?php
	include_once '../config.php';
	include_once 'dataHandler.php';

	$profile = dataGet($urlBackend ."/users/".$_SESSION["user"]);
	
	$profile["Firstname"] = $_POST["Firstname"];
	$profile["Lastname"] = $_POST["Lastname"];
	$profile["Bio"] = $_POST["Bio"];

	$result = dataPatch($urlBackend ."/users/". $_SESSION["user"], $profile);

	header("Location: /profile?".$_SESSION["user"]);
?>