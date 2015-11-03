<?php
	header('Location: /profile');

	include 'dataHandler.php';
	//Tu adres do wyslałania
	$url = "http://" . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URl'];	//??
	$jsonData = array(
		$_POST["username"] => "username";
		$_POST["password"] => "password";
		$_POST["email"] => "email";
	);
	
	$result = dataSend($url,$jsonData);
	
	//Odpowiedź z adresu
	$url2 = '';
	$answer = dataGet($url2);
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
