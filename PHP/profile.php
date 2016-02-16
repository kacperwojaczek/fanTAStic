<?php
	include_once "config.php";
	include_once "structure/editable.php";

	$user = '';
	if(!$_SERVER['QUERY_STRING']) {
		if(!$_SESSION) header("Location: /");
		else $user .= $_SESSION["user"];
	} else $user .= $_SERVER['QUERY_STRING'];

	$profile = dataGet($urlBackend."/users/".$user);
	if(!$profile) header("Location: /");
?>

<html>
<?php echo head($profile["Login"], "profile"); ?>
<body>
	<div id="modal-control" class="hidden">
	<?php echo navbar() ?>
	<main>
		<div class="profile">
			<header>
				<?php echo avatar("profile", $user,  $profile["Firstname"] ." ". $profile["Lastname"]) ?>
				<div class="info">
					<a class="login" href="/home?<?php echo $profile["Login"]?>"><?php echo $profile["Login"]?></a>
					<div id="firstname" class="name first"<?php echo editable()?>><?php echo $profile["Firstname"] ?></div><div id="lastname" class="name last"<?php echo editable()?>><?php echo $profile["Lastname"] ?></div>
					<q id="bio" class="bio"<?php echo editable()?>><?php echo $profile["Bio"] ?></q>
				</div>
			</header>
			<section>
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

						echo postExcerpt($author, $authorUrl, $postUrl, $postTitle, $postText, $postDate);
					}
				?>
			</section>
			<footer>
				<?php

				?>
			</footer>
		</div>
	</main>
	<?php echo footer(); ?>
	</div>
	<div id="modal-wrapper" class="hidden">
	</div>
</body>
</html>