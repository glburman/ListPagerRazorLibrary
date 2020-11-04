const gulp = require('gulp');
const sass = require('gulp-sass');
const concat = require('gulp-concat');

gulp.task('pager', function () {
    return gulp.src([
        'wwwroot/scss/pager.scss',
        'wwwroot/scss/pager-theme-default.scss'])
        .pipe(sass())
        .pipe(concat('pager.css'))
        .pipe(gulp.dest('wwwroot/css'))
})

gulp.task('pager-dark', function () {
    return gulp.src([
        'wwwroot/scss/pager.scss',
        'wwwroot/scss/pager-theme-dark.scss'])
        .pipe(sass())
        .pipe(concat('pager-dark.css'))
        .pipe(gulp.dest('wwwroot/css'))
})

gulp.task('pager-neon', function () {
    return gulp.src([
        'wwwroot/scss/pager.scss',
        'wwwroot/scss/pager-theme-neon.scss'])
        .pipe(sass())
        .pipe(concat('pager-neon.css'))
        .pipe(gulp.dest('wwwroot/css'))
})

gulp.task('default', gulp.series(['pager', 'pager-dark', 'pager-neon']));
