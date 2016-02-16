<?php
include_once "editable.php";
function postExcerpt($author = "__AUTHOR__", $authorUrl = "__AUTHOR URL__", $postUrl = "__POST URL__", $postTitle = "__POST TITLE__", $postText = "__POST TEXT__", $postDate = "0000-00-00T00:00:00", $postId = "") {
	global $loggedIn;

	if(!$loggedIn){
	return '
		<div class="post excerpt">
			<header>'.
				avatar("post", $authorUrl, $author)
				.'<a class="post-title" href="/single-post?'. $postUrl .'"><h2>'. $postTitle .'</h2></a>
			</header>
			<article>
				'. substr($postText, 0, 256) .'...
			</article>
			<footer>
				'. date("l, F jS Y, H:i", strtotime($postDate)).'
			</footer>
		</div>
	';
	} else {
	return '
		<div class="post excerpt">
			<header>'.
				avatar("post", $authorUrl, $author)
				.'<a class="post-title" href="/single-post?'. $postUrl .'"><h2>'. $postTitle .'</h2></a>
			</header>
			<article>
				'. substr($postText, 0, 256) .'...
			</article>
			<footer>
				'. date("l, F jS Y, H:i", strtotime($postDate)).'
			</footer>
			<form action="../functions/shareAction.php" method="Post">
				<input type="hidden" name="id" value="'.$postId.'">
				<input type="submit" value="Share">
			</form>
		</div>
	';
	}
}

function postFull($author = "__AUTHOR__", $authorUrl = "__AUTHOR URL__", $postUrl = "__POST URL__", $postTitle = "__POST TITLE__", $postText = "__POST TEXT__", $postDate = "0000-00-00T00:00:00", $postId = "") {
	global $loggedIn;

	if(!$loggedIn) {
	return '
		<div class="post full">
			<header>
				<a class="post-title" href="/single-post?'. $postUrl .'"><h2'.editable().'>'. $postTitle .'</h2></a>
				<a class="post-author" href="/home?'. $authorUrl .'"><h3>'. $authorUrl .'</h3></a>
			</header>
			<article '.editable().'>
				'.$postText .'
			</article>
			<footer>'.
				avatar("small", $authorUrl, $author).
				'<div class="post-date">'. date("l, F jS Y, H:i", strtotime($postDate)).'</div>
			</footer>
		</div>
	';
	} else {
	return '
	<div class="post full">
			<header>
				<a class="post-title" href="/single-post?'. $postUrl .'"><h2'.editable().'>'. $postTitle .'</h2></a>
				<a class="post-author" href="/home?'. $authorUrl .'"><h3>'. $authorUrl .'</h3></a>
			</header>
			<article '.editable().'>
				'.$postText .'
			</article>
			<footer>'.
				avatar("small", $authorUrl, $author).
				'<div class="post-date">'. date("l, F jS Y, H:i", strtotime($postDate)).'</div>
				<form action="../functions/shareAction.php" method="Post">
					<input type="hidden" name="id" value="'.$postId.'">
					<input type="submit" value="Share">
				</form>
			</footer>
		</div>
		';
	}
}
?>