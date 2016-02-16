<?php
	include_once "config.php";
	require_once "structure/post.php";

	$user = '';
	if(!$_SERVER['QUERY_STRING']) {
		if(!$_SESSION) header("Location: /");
		else $user .= $_SESSION["user"];
	} else $user .= $_SERVER['QUERY_STRING'];

	$profile = dataGet($urlBackend."/users/".$user);
	if(!$profile) header("Location: /");
?>

<html>
<?php echo head($user, "home"); ?>
<body>
	<div id="modal-control" class="hidden">
		<?php echo navbar();?>
		<main>
			<div id="new-post">
				<?php
					if($loggedIn && $_SESSION["user"] === $user) {
						$author = $profile["Firstname"]." ". $profile["Lastname"];
						$authorUrl = $profile["Login"];

						$postTitle = "New Post";
						$postUrl = "";

						$postText = "Write your story";

						$postDate = date("l, F jS Y, H:i");

						echo postFull($author, $authorUrl, $postUrl, $postTitle, $postText, $postDate, $postUrl);
					}
				?>
			</div>
			<div id="old-posts">
				<?php
					$posts = dataGet($urlBackend ."/users/". $user ."/posts");
					$index = count($posts);

					while($index > 0) {
						$post = dataGet($urlBackend ."/posts/". $posts[--$index])[0];

						$author = $profile["Firstname"]." ". $profile["Lastname"];
						$authorUrl = $profile["Login"];

						$postTitle = $post["title"];
						$postUrl = $post["id"];

						$postText = $post["text"];

						$postDate = $post["date"];

						echo postFull($author, $authorUrl, $postUrl, $postTitle, $postText, $postDate, $postUrl);
					}
				?>
			</div>
		</main>
	<?php echo footer(); ?>
	</div>
	</div>
	<div id="modal-wrapper" class="hidden">
	</div>
</body>
</html>