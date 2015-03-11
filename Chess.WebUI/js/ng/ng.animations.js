angular.module('app.animations', []).animation('.slide-animation', function () {
	return {
		enter: function (element, done) {
			jQuery(element).css({
				position: 'relative',
				'z-index': 100,
				left: 600,
				opacity: 0
			});
			jQuery(element).animate({
				left: 0,
				opacity: 1
			}, done);
		},

		leave: function (element, done) {
			jQuery(element).css({
				position: 'relative',
				'z-index': 101,
				left: 0,
				opacity: 1
			});
			jQuery(element).animate({
				left: -600,
				opacity: 0
			}, done);
		}
	};
})
    .animation('.repeat-animation', function () {
    	return {
    		enter: function (element, done) {
    			jQuery(element).css({
    				position: 'relative',
    				left: -10,
    				opacity: 0
    			});
    			jQuery(element).animate({
    				left: 0,
    				opacity: 1
    			}, done);
    		},

    		leave: function (element, done) {
    			jQuery(element).css({
    				position: 'relative',
    				left: 0,
    				opacity: 1
    			});
    			jQuery(element).animate({
    				left: -10,
    				opacity: 0
    			}, done);
    		},

    		move: function (element, done) {
    			jQuery(element).css({
    				opacity: 0.5
    			});
    			jQuery(element).animate({
    				opacity: 1
    			}, done);
    		}
    	};
    })
    .animation('.show-hide-animation', function () {
    	return {
    		beforeAddClass: function (element, className, done) {
    			if (className == 'ng-hide') {
    				jQuery(element).animate({
    					opacity: 0
    				}, done);
    			}
    			else {
    				done();
    			}
    		},
    		removeClass: function (element, className, done) {
    			if (className == 'ng-hide') {
    				element.css('opacity', 0);
    				jQuery(element).animate({
    					opacity: 1
    				}, done);
    			}
    			else {
    				done();
    			}
    		}
    	};
    })
    .animation('.switch-animation', function () {
    	return {
    		enter: function (element, done) {
    			element = jQuery(element);
    			element.css({
    				position: 'absolute',
    				height: 500,
    				left: element.parent().width()
    			});
    			element.animate({
    				left: 0
    			}, done);
    		},

    		leave: function (element, done) {
    			element = jQuery(element);
    			element.css({
    				position: 'absolute',
    				height: 500,
    				left: 0
    			});
    			element.animate({
    				left: -element.parent().width()
    			}, done);
    		}
    	};
    })
    .animation('.toggle-animation', function () {
    	return {
    		beforeAddClass: function (element, className, done) {
    			if (className == 'disabled') {
    				jQuery(element).animate({
    					'color': '#666666',
    					'background': '#AAAAAA'
    				}, done);
    			}
    			else {
    				done();
    			}
    		},

    		beforeRemoveClass: function (element, className, done) {
    			if (className == 'disabled') {
    				jQuery(element).animate({
    					'color': '#000000',
    					'background': '#FFFFFF'
    				}, done);
    			}
    			else {
    				done();
    			}
    		}
    	};
    });
