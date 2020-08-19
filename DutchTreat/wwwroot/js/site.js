$('document').ready(function () {
	var contactForm = $(".contact-form").get(0);
	contactForm.hidden = true;

	var buyButton = $("#buy-button");
	//buyButton.on("click", function () {
	//	contactForm.hidden = !contactForm.hidden;
	//})

	var productProp = $(".product-prop-list li");
	productProp.on("click", function () {
		console.log("I clicked on " + $(this).text());
	});

	var loginToggle = $(".login-toggle");
	var popupForm = $(".popup-form");

	loginToggle.on("click", function () {
		//popupForm.toggle(500);
		//popupForm.slideToggle(500);
		popupForm.fadeToggle(800);
	});
});