<?php
	include "config.php";
?>

<html>
<?php head(); ?>
<body>
<?php if(modalCookieCheck("modal-launch")) { ?>
	<div id="modal-control">
<?php } ?>
	<?php navbar(); ?>
	<main>
		<?php halloffame(); ?>
	</main>
	<?php footer(); ?>
<?php if(modalCookieCheck("modal-launch")) { ?>
	</div>
	<div id="modal-wrapper">
		<div id="modal">
			<?php logo("", ""); ?>
			<?php login(); ?>
			<?php register(); ?>
			<div id="explore">
				<h2>or</h2>
				<div id="modal-close">Explore the Community</div>
			</div>
		</div>
	</div>
<?php } ?>
</body>
</html>