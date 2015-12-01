<?php
function logo($size ="", $color="") {
	global $site;
	return '
		<a class="logo'.' '.$size.' '.$color .'" href="/">'.$site.'</a>
	';
}
?>