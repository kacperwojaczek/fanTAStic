<?php
	//header('Location: /profile');

	include 'dataHandler.php';
	
	$url = $urlBackend + "/login";

	$jsonData = array(
		$_POST["Login"] => "username",
		$_POST["Password"] => "password",
	);

	$result = dataSend($url, $jsonData);
	
	$url2 = '';
	$answer = dataGet($url2);
	$answerDecoded = json_decode($answer);

	var_dump($answer);
?>