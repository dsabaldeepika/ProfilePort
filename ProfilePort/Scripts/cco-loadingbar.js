//****************************************************************
//Desenvolvido por Cesar Oliveira <cesar@ccosolutions.com.br>
//CCO Solutions -  www.ccosolutions.com.br <contato@ccosolutions.com.br>
//Version 1.0.0
//Dependences:
//  - JQuery 1.10.2
//  - Bootstrap 3.0.3
//****************************************************************
//$('body').loadingbar();

(function ($) {
	$.fn.loadingbar = function (options) {
		return this.each(function () {
			var $this = $(this),
				data = $this.data('LoadingBar');
			if (!data) {
				$this.data('LoadingBar', new LoadingBar(this, options));
			}
		});
	};

	LoadingBar = function (element, options) {
		var d = $.fn.loadingbar.defaults;
		var myElement = '<div class="progress ' + (d.pgbStriped == true ? 'progress-striped' : '') + ' ' + (d.pgbActive == true ? 'active' : '') + ' " id="pgbLoading"' +
			'style="height: ' + d.height + '; position: ' + d.position + '; top: ' + d.top + '; left: ' + d.left + '; z-index: ' + d.zindex + '; width: ' + d.width + ';">' +
			'<div class="progress-bar progress-bar-' + (d.pgbClass != '' ? d.pgbClass : 'info') + '" role="progressbar" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100" style="width: 100%"><span class="sr-only">20% Complete</span>     </div>' +
		'</div>';
		$(element).append(myElement);

		Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequest);
		Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest)
		function beginRequest(sender, args) {
			ProgressLoad(true);
		} function endRequest(sender, args) {
			ProgressLoad(false);
		}
		function ProgressLoad(start) {
			if (start)
				$('#pgbLoading').stop().css('width', '0%').animate({ width: '100%' }, 1000);
			else
				$('#pgbLoading').stop().animate({ width: '100%' }, 'slow', function () {
					$(this).css('width', '0%');
				});
		}
	}

	$.fn.loadingbar.defaults = {
		pgbActive: true,
		pgbStriped: true,
		pgbClass: 'info',
		position: '',
		height: '8px',
		position: 'fixed',
		top: '0px !important',
		left: '0px !important',
		zindex: '99999',
		width: '0%'
	};
})(jQuery);