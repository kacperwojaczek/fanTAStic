<?php
	include_once "config.php";
	
	require_once("structure/post.php");
	require_once("structure/lipsum.php");
?>

<html>
<?php echo head("__BLOG_TITLE__"); ?>
<body>
	<!--?php //include navbar ?-->
	<main class="aside">
		<!--?php //post(all) ?-->
		<?php echo writePost(); ?>

		<?php for($i = 0; $i < rand(5, 30); ++$i) {
			$string = implode(' ', array_slice(explode(' ', $lipsum), 0, rand(5, 100)));
			echo postFull("", "", "", "", $string);
		} ?>
	</main>
	<aside>
	</aside>
	<!--?php footer(); ?-->
</body>
</html>