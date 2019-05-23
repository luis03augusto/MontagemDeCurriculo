﻿///// <binding BeforeBuild='default' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');
var rimraf = require("rimraf");
var merge = require('merge-stream');
var csso = require('gulp-csso');

// Dependency Dirs
var dependenciesDirectory = require("./dependencies.Directory.json");
var package = require("./package.json");
var dependencies = package.dependencies;

gulp.task("minify-js", function () {

    var streams = [
        gulp.src(["wwwroot/js/*.js"])
            .pipe(uglify())
            .pipe(concat("site.min.js"))
            .pipe(gulp.dest("wwwroot/libs/site/js"))
    ];

    return merge(streams);
});

gulp.task("minify-css", function () {

    return gulp.src('wwwroot/css/site.css')
        .pipe(csso())
        .pipe(concat("site.min.css"))
        .pipe(gulp.dest('wwwroot/libs/site/css'));
});

gulp.task("minify", gulp.parallel('minify-js', 'minify-css'));

gulp.task("clean-vendor", function (cb) {
    return rimraf("wwwroot/vendor/", cb);
});

gulp.task("clean-site", function (cb) {
    return rimraf("wwwroot/libs/site/", cb);
});

gulp.task("clean", gulp.parallel('clean-vendor', 'clean-site'));

gulp.task("scripts", function () {

    var streams = [];

    for (var prop in dependencies) {
        console.log("Prepping Scripts for: " + prop);
        if (prop in dependenciesDirectory) {
            for (var itemProp in dependenciesDirectory[prop]) {
                streams.push(gulp.src("node_modules/" + prop + "/" + itemProp)
                    .pipe(gulp.dest("wwwroot/vendor/" + prop + "/" + dependenciesDirectory[prop][itemProp])));
            }
        } else {
            streams.push(gulp.src(["node_modules/" + prop + "/**/*", "node_modules/" + prop + "/*"])
                .pipe(gulp.dest("wwwroot/vendor/" + prop + "/")));
        }
    }

    return merge(streams);
});

gulp.task('watch', function () {
    gulp.watch(['wwwroot/css/site.css', "wwwroot/js/*.js"]).on('change', function (file) {
        console.log('file changed: ', file);
        gulp.series("clean-site", "minify")();
    });
});

gulp.task('default',
    gulp.series('clean', gulp.parallel('minify', 'scripts'))
);