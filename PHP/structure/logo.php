<?php
function logo($size ="", $color="") {
	global $site;
	?>
		<a class="logo<?php if($size !== "") echo " ".$size; if($color !== "") echo " ".$color; ?>" href="/"><?php echo $site; ?></a>
	<?php
}
?>