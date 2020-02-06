/// <binding BeforeBuild='default' />
const gulp = require('gulp');
const sass = require('gulp-sass');
const del = require('del');
const concat = require('gulp-concat');
const cleanCss = require('gulp-clean-css');



gulp.task('pager', function () {
    return gulp.src([
        'wwwroot/scss/pager-theme-default.scss',
        'wwwroot/scss/pager.scss'])
        .pipe(sass())
        .pipe(concat('pager.css'))
        .pipe(cleanCss({ level: { 2: { restructureRules: true } } }))
        .pipe(gulp.dest('wwwroot/css'))
})

gulp.task('pager-dark', function () {
    return gulp.src([
        'wwwroot/scss/pager.scss',
        'wwwroot/scss/pager-theme-dark.scss'])
        .pipe(sass())
        .pipe(concat('pager-dark.css'))
        .pipe(cleanCss({ level: { 2: { restructureRules: true } } }))
        .pipe(gulp.dest('wwwroot/css'))
})

gulp.task('pager-neon', function () {
    return gulp.src([
        'wwwroot/scss/pager.scss',
        'wwwroot/scss/pager-theme-neon.scss'])
        .pipe(sass())
        .pipe(concat('pager-neon.css'))
        .pipe(cleanCss({ level: { 2: { restructureRules: true } } }))
        .pipe(gulp.dest('wwwroot/css'))
})

gulp.task('default', gulp.series(['pager', 'pager-dark', 'pager-neon']));
