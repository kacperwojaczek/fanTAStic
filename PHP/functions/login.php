<?php

	header('Location: /profile');

	include 'dataHandler.php';
	//"http://" . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URl'];
	//"http://$_SERVER[HTTP_HOST]$_SERVER[REQUEST_URI]";
	
	echo $urlBackend;

	$url = $urlBackend;
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

	var_dump($answer);
?>