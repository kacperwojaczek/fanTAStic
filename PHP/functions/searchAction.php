<?php
	include_once '../config.php';
	include_once 'dataHandler.php';

	$result = dataGet($urlBackend ."/tags/".$_POST["query"]);

	var_dump($result);
?>