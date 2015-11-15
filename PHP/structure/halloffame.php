<?php
function halloffame() {
	require_once("post.php");
	require_once("lipsum.php");

	?>
	<div class="post-container">
	<?php
	for($i = 0; $i < rand(5, 100); ++$i) {
		$string = implode(' ', array_slice(explode(' ', $lipsum), 0, rand(5, 100)));
		postExcerpt("", "", "", "", $string);
	}
	?>
	</div>
	<?php
}
?>