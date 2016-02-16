<?php
	include "structure/post.php";
	include "config.php";

	$post = '';
	if(!$_SERVER['QUERY_STRING']) {
		header("Location: /");
	} else $post .= $_SERVER['QUERY_STRING'];

	$post = dataGet($urlBackend."/posts/".$post);
	if(!$post) header("Location: /");
	$post = $post[0];

	$profile = dataGet($urlBackend."/users/".$post["authorId"]);
?>

<html>
<?php echo head($profile["Login"], $post["title"]); ?>
<body>
	<div id="modal-control" class="hidden">
	<?php echo navbar() ?>
	<main>
	<?php
		$author = $profile["Firstname"]." ". $profile["Lastname"];
		$authorUrl = $profile["Login"];

		$postTitle = $post["title"];
		$postUrl = $post["id"];

		$postText = $post["text"];

		$postDate = $post["date"];

		echo postFull($author, $authorUrl, $postUrl, $postTitle, $postText, $postDate, $postUrl);
	?>
	</main>
	<?php echo footer(); ?>
	</div>
	<div id="modal-wrapper" class="hidden">
	</div>
</body>
</html>