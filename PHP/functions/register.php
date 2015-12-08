<?php

	//header('Location: /');

	include 'dataHandler.php';

	$url = $urlBackend + "/register";

	$jsonData = array(
		$_POST["Username"] => "username",
		$_POST["Password"] => "password",
		$_POST["Email"] => "email",
	);
	
	$result = dataSend($url, $jsonData);
	
	$url2 = '';
	$answer = dataGet($url2);
	$answerDecoded = json_decode($answer);

	var_dump($answer);
?>
