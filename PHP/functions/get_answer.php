<?php
function get_answer($url2)
{
  $ch2 = curl_init($url2);
	curl_setopt_array($ch2, array(
	  //CURLOPT_RETURNTRANSFER => 1,	//Powoduje zwrócenie true(1) jeśli uda się połączyć i otrzymamy odpowiedź
		CURLOPT_URL = $url2		          //Jeśli się zakomentuje RETURNTRANSFER to chyba dostaniemy string w odpowiedzi
	));
	$answer = curl_exec($ch2);
	return $answer
}
?>
