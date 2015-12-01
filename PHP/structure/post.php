<?php
function postExcerpt($author = "", $authorUrl = "", $postUrl = "", $postTitle = "", $postText = "") {
	return '
		<div class="post">
			<header>
				<a class="avatar" href="'. $authorUrl .'">'. $author .'</a>
				<a class="post-title" href="'. $postUrl .'"><h2>'. $postTitle .'</h2></a>
			</header>
			<article>
				'. $postText .'...
			</article>
			<footer>
			</footer>
		</div>
	';
}
?>