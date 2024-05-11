function simpleConfirm(vMessage, vType, vTitle, vCallback, vBackdrop, vTextBtn) {
	/*Optional Parameters*/
	if (vBackdrop == undefined) {
		vBackdrop = true;
	}

	if (vTextBtn === undefined) {
		vTextBtn = "OK";
	}

	var dialog = bootbox.alert({
		title: vTitle,
		message: vMessage,
		buttons: {
			ok: {
				label: vTextBtn
			}
		},
		backdrop: vBackdrop,
		callback: vCallback
	});
	switch (vType) {
		case 1:
			$(".bootbox .modal-header").addClass("bg-dark");
			$(".bootbox .modal-footer button").removeClass("bg-dark");
			$(".bootbox .modal-footer button").addClass("bg-dark");
			break;
		case 2:
			$(".bootbox .modal-header").addClass("bg-danger");
			$(".bootbox .modal-footer button").removeClass("bg-primary");
			$(".bootbox .modal-footer button").addClass("bg-danger");
			break;
		case 3: //Not used yet
			$(".bootbox .modal-header").addClass("bg-success");
			$(".bootbox .modal-footer button").removeClass("bg-primary");
			$(".bootbox .modal-footer button").addClass("bg-success");
			break;
		case 4: //Not used yet
			$(".bootbox .modal-header").addClass("bg-warning");
			$(".bootbox .modal-footer button").removeClass("bg-primary");
			$(".bootbox .modal-footer button").addClass("bg-warning");
			break;
		case 5: //Not used yet
			$(".bootbox .modal-header").addClass("bg-info");
			$(".bootbox .modal-footer button").removeClass("bg-primary");
			$(".bootbox .modal-footer button").addClass("bg-info");
			break;
	}

	dialog.on('hidden.bs.modal', function (event) {
		setTimeout(function () { $('body').addClass('modal-open'); }, 500);
	});
}


function confirmYesNo(vMessage, vType, vTitle, vCallback, vTextBtn1, vTextBtn2) {
	/*Optional Parameters*/
	if (vTextBtn1 === undefined) {
		vTextBtn1 = "OK";
	}

	if (vTextBtn2 === undefined) {
		vTextBtn2 = "Cancelar";
	}

	var dialog = bootbox.confirm({
		title: vTitle,
		message: vMessage,
		buttons: {
			confirm: {
				label: vTextBtn1
			},
			cancel: {
				label: vTextBtn2,
				className: "btn-cancel"
			}
		},
		callback: vCallback
	});
	switch (vType) {
		case 1:
			$(".bootbox .modal-header").addClass("bg-dark");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").removeClass("bg-dark");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").addClass("bg-dark");
			break;
		case 2:
			$(".bootbox .modal-header").addClass("bg-danger");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").removeClass("bg-primary");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").addClass("bg-danger");
			break;
		case 3:
			$(".bootbox .modal-header").addClass("bg-success");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").removeClass("bg-primary");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").addClass("bg-success");
			break;
		case 4:
			$(".bootbox .modal-header").addClass("bg-warning");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").removeClass("bg-primary");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").addClass("bg-warning");
			break;
		case 5:
			$(".bootbox .modal-header").addClass("bg-info");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").removeClass("bg-primary");
			$(".bootbox .modal-footer button[data-bb-handler='confirm']").addClass("bg-info");
			break;
	}

	dialog.on('hidden.bs.modal', function (event) {
		setTimeout(function () { $('body').addClass('modal-open'); }, 500);
	});
}
