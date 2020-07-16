require.config({
    waitSeconds: 45,
    baseUrl: 'lib',
    paths: {
        hd: '../js/hd',
        js: '../js',
        'domReady': 'requirejs_domReady/domReady',
        'jquery': "kendo_ui/js/jquery.min",
        'jquery.validate': 'jquery_validation/dist/jquery.validate',
        'jquery.validate.unobtrusive': 'jquery_validation_unobtrusive/jquery.validate.unobtrusive',
        'jquery.livequery': 'jquery_livequery/dist/jquery.livequery',
        'toolkit': "dashboard/dist/toolkit",
        'js.cookie': "js_cookie/src/js.cookie",
        'kendo': 'kendo/js',
        'kendo.all.min': 'kendo_ui/js/kendo.all.min',
        'kendo.aspnetmvc.min': 'kendo/js/kendo.aspnetmvc.min',
        'kendo.timezones.min': 'kendo/js/kendo.timezones.min',
        'blockui': 'jquery_blockui/jquery.blockUI',
        'pako_deflate.min': 'kendo/js/pako_deflate.min',
        'less': 'less/dist/less.min',
        'bootstrap': 'bootstrap/dist/js/bootstrap.min',
        'rangy':'ckeditor/plugins/lite/js/rangy-core',
        'ckeditor': 'ckeditor/ckeditor',
        'datatables.net': 'datatables.net/datatables.min',
        'datatables.bootstrap': 'datatables.net/DataTables/js/dataTables.bootstrap.min'
    },
    bundles: {
        'kendo.all.min': [
            '0rpicker.min',
            'kendo.columnmenu.min',
            'kendo.columnsorter.min',
            'kendo.combobox.min',
            'kendo.core.min',
            'kendo.data.min',
            'kendo.data.odata.min',
            'kendo.data.signalr.min',
            'kendo.data.xml.min',
            'kendo.dataviz.barcode.min',
            'kendo.dataviz.chart.funnel.min',
            'kendo.dataviz.chart.min',
            'kendo.dataviz.chart.polar.min',
            'kendo.dataviz.core.min',
            'kendo.dataviz.diagram.min',
            'kendo.dataviz.gauge.min',
            'kendo.dataviz.map.min',
            'kendo.dataviz.min',
            'kendo.dataviz.mobile.min',
            'kendo.dataviz.qrcode.min',
            'kendo.dataviz.sparkline.min',
            'kendo.dataviz.stock.min',
            'kendo.dataviz.themes.min',
            'kendo.dataviz.treemap.min',
            'kendo.datepicker.min',
            'kendo.datetimepicker.min',
            'kendo.dom.min',
            'kendo.draganddrop.min',
            'kendo.drawing.min',
            'kendo.dropdownlist.min',
            'kendo.editable.min',
            'kendo.editor.min',
            'kendo.excel.min',
            'kendo.filebrowser.min',
            'kendo.filtercell.min',
            'kendo.filtermenu.min',
            'kendo.fx.min',
            'kendo.gantt.list.min',
            'kendo.gantt.min',
            'kendo.gantt.timeline.min',
            'kendo.grid.min',
            'kendo.groupable.min',
            'kendo.imagebrowser.min',
            'kendo.list.min',
            'kendo.listview.min',
            'kendo.maskedtextbox.min',
            'kendo.menu.min',
            'kendo.mobile.actionsheet.min',
            'kendo.mobile.application.min',
            'kendo.mobile.button.min',
            'kendo.mobile.buttongroup.min',
            'kendo.mobile.collapsible.min',
            'kendo.mobile.drawer.min',
            'kendo.mobile.listview.min',
            'kendo.mobile.loader.min',
            'kendo.mobile.min',
            'kendo.mobile.modalview.min',
            'kendo.mobile.navbar.min',
            'kendo.mobile.pane.min',
            'kendo.mobile.popover.min',
            'kendo.mobile.scroller.min',
            'kendo.mobile.scrollview.min',
            'kendo.mobile.shim.min',
            'kendo.mobile.splitview.min',
            'kendo.mobile.switch.min',
            'kendo.mobile.tabstrip.min',
            'kendo.mobile.view.min',
            'kendo.multiselect.min',
            'kendo.notification.min',
            'kendo.numerictextbox.min',
            'kendo.ooxml.min',
            'kendo.pager.min',
            'kendo.panelbar.min',
            'kendo.pdf.min',
            'kendo.pivot.configurator.min',
            'kendo.pivot.fieldmenu.min',
            'kendo.pivotgrid.min',
            'kendo.popup.min',
            'kendo.progressbar.min',
            'kendo.reorderable.min',
            'kendo.resizable.min',
            'kendo.responsivepanel.min',
            'kendo.router.min',
            'kendo.scheduler.agendaview.min',
            'kendo.scheduler.dayview.min',
            'kendo.scheduler.min',
            'kendo.scheduler.monthview.min',
            'kendo.scheduler.recurrence.min',
            'kendo.scheduler.timelineview.min',
            'kendo.scheduler.view.min',
            'kendo.selectable.min',
            'kendo.slider.min',
            'kendo.sortable.min',
            'kendo.splitter.min',
            'kendo.spreadsheet.min',
            'kendo.tabstrip.min',
            'kendo.timepicker.min',
            'kendo.toolbar.min',
            'kendo.tooltip.min',
            'kendo.touch.min',
            'kendo.treelist.min',
            'kendo.treeview.draganddrop.min',
            'kendo.treeview.min',
            'kendo.upload.min',
            'kendo.userevents.min',
            'kendo.validator.min',
            'kendo.view.min',
            'kendo.virtuallist.min',
            'kendo.web.min',
            'kendo.webcomponents.min',
            'kendo.window.min',
        ]
    },
    shim: {
        'toolkit': ["jquery"],
        'jquery.validate': ['jquery'],
        'jquery.validate.unobtrusive': ['jquery.validate'],
        'ckeditor': ['jquery']
    }
});
//require.nameToUrl_origin = typeof require.nameToUrl_origin == "undefined" ? require.s.contexts._.nameToUrl : require.nameToUrl_origin;
//require.s.contexts._.nameToUrl = function (a, b, c) {
//    if (a.startsWith("kendo.")) {
//        a = "kendo/" + a;
//    }
//    return require.nameToUrl_origin(a, b, c);
//}
//# sourceMappingURL=config.js.map