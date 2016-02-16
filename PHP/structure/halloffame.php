<?php
function halloffame() {
	include_once "post.php";
	include "config.php";

	$result = "<div class=\"post-container\">";

	$posts = dataGet($urlBackend ."/posts/0/");

	if($posts){
		foreach ($posts as $post) {
			$profile = dataGet($urlBackend ."/users/". $post["authorId"]);

			$author = $profile["Firstname"]." ". $profile["Lastname"];
			$authorUrl = $profile["Login"];

			$postTitle = $post["title"];

			$postUrl = $post["id"];

			$postText = $post["text"];

			$postDate = $post["date"];

			$result .= postExcerpt($author, $authorUrl, $postUrl, $postTitle, $postText, $postDate);
		}
	}
	$result .= "</div>";
	return $result;
}
?>