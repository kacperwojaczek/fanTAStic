<?php

	header('Location: /profile');

	include 'dataHandler.php';
	//"http://" . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URl'];
	//"http://$_SERVER[HTTP_HOST]$_SERVER[REQUEST_URI]";
	
	$url = "http://" . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];	//?
	$jsonData = array(
		$_POST["username"] => "username";
		$_POST["password"] => "password";
	);
	$result = dataSend($url, $jsonData);
	
	$url2 = '';
	$answer = get_answer($url2);
	$answerDecoded = json_decode($answer);
	/*
	Proponowana przez Grega odpowiedź:
	{
		id:
		msg:
		params:
		info:
	}
	*/
	/*
	Warunki co ma się stać w zależności od odpowiedzi.
	*/
	die();
?>
