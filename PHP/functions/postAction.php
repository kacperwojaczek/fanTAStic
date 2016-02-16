<?php
	include_once '../config.php';
	include_once 'dataHandler.php';

	$result = dataSend($urlBackend ."/users/". $_SESSION["user"] ."/posts", $_POST);

	header("Location: /home?".$_SESSION["user"]);
?>