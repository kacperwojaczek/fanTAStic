<?php
function dataSend($url)
{
	$ch =  curl_init($url);
	$jsonData = array(
		$_POST["username"] => "username";
		$_POST["password"] => "password";
	);
	$jsonDataEncoded = json_encode($jsonData);
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonDataEncoded);
	curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json'));
	$result = curl_exec($ch);
	curl_close($ch);
	return $result;
}

function dataGet($url2)
{
  	$ch2 = curl_init($url2);
	curl_setopt_array($ch2, array(
	  	//CURLOPT_RETURNTRANSFER => 1,	//Powoduje zwrócenie true(1) jeśli uda się połączyć i otrzymamy odpowiedź
		CURLOPT_URL = $url2		          //Jeśli się zakomentuje RETURNTRANSFER to chyba dostaniemy string w odpowiedzi
	));
	$answer = curl_exec($ch2);
	return $answer;
}
?>
