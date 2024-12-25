﻿using FinalBlog.DATA.Models;
using FinalBlog.ViewModels.Article;

namespace FinalBlog.Services.Interfaces
{
    public interface IArticleService
    {
        public Task<ResultModel> AddArticle(ArticleAddViewModel model);
        public Task<ArticleViewModel> GetArticleById(int articleId);
        //public Task<ArticleViewModel> GetArticleByName(string articleName);
        public Task<ResultModel> UpdateArticle(ArticleEditViewModel model);
        public Task<ResultModel> DeleteArticle(int articleId);
        public ArticleListViewModel GetAllArticles();
        public ArticleListViewModel GetArticlesOfAuthor(string authorId);
    }
}
