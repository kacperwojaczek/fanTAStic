<?php
function postExcerpt($author = "", $authorUrl = "", $postUrl = "", $postTitle = "", $postText = "") {
	?>
		<div class="post">
			<header>
				<a class="avatar" href="<?php echo $authorUrl;?>"><?php echo $author;?></a>
				<a class="post-title" href="<?php echo $postUrl;?>"><h2><?php echo $postTitle;?></h2></a>
			</header>
			<article>
				<?php echo $postText;?>...
			</article>
			<footer>
			</footer>
		</div>
	<?php
}
?>