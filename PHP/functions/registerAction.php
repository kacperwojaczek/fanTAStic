<?php
	include_once '../config.php';
	include_once 'dataHandler.php';

	$url = $urlBackend."/register/" .$_POST["username"];
	
	$json = array(
		"Login" => $_POST["username"],
		"Firstname" => "",
		"Lastname" => "",
		"Password" => $_POST["password"],
		"Email" => $_POST["email"]
	);
	$result = dataSend($url, $json);

	if($result['http_code'] === 201) {
		header('Location: /?success#register');
	} else {
		header('Location: /?error'.$result['http_code'].'#register');
	}
?>
