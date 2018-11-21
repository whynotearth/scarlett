"use strict";
var gulp = require("gulp"),
    sass = require("gulp-sass");

var root = "./wwwroot/";
var paths = {
    scss: root + "scss/**/*.scss",
    css: root + "css",
    node_modules: "./node_modules"    
};

gulp.task("scss", function(){
    return gulp.src(paths.scss)
        .pipe(sass.sync({
            includePaths: paths.node_modules
        }).on("error", sass.logError))
        .pipe(gulp.dest(paths.css));
});

gulp.task("scss:watch", function() {
    return gulp.watch(paths.scss, ["scss"]);
});

gulp.task("default", ["scss"]);
gulp.task("watch", ["scss:watch"]);