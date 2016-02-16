
<?php
	include_once '../config.php';
	include_once 'dataHandler.php';

	$result = dataGet($urlBackend ."/users/".$_SESSION["user"]."/friends");
	
	var_dump($result);

	// header("Location: /profile?".$_SESSION["user"]);
?>