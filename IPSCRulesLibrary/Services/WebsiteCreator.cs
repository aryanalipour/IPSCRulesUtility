﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using IPSCRulesLibrary.ObjectClasses;

namespace IPSCRulesLibrary.Services
{
    public class WebsiteCreator
    {
        private HtmlParser _htmlParser;

        public WebsiteCreator()
        {
            _htmlParser = new HtmlParser();
        }

        public void CreateWebsiteFilesDirectory(List<Discipline> disciplines)
        {
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            var websitePath = $"{rootPath}/website";

            var htmlContent = _htmlParser.CreateIndexHtmlPage(disciplines);
            var cssStyling = _htmlParser.CreateCssStyling();

            UtilityHelper.CreateUpdateFile(websitePath, "index.html", htmlContent);
            UtilityHelper.CreateUpdateFile(websitePath, "styling.css", cssStyling);

            foreach (var discipline in disciplines)
            {
                var disciplinePath = $"{websitePath}/{discipline.Name}";

                CreateDisciplineFilesDirectory(disciplinePath, discipline);
            }
        }

        public void CreateDisciplineFilesDirectory(string disciplinePath, Discipline discipline)
        {
            var filename = $"{discipline.Name}.html";
            var htmlContent = _htmlParser.CreateDisciplineHtmlPage(discipline);

            UtilityHelper.CreateUpdateFile(disciplinePath, filename, htmlContent);

            foreach (var chapter in discipline.Chapters)
            {
                var chapterPath = $"{disciplinePath}";

                CreateChapterFilesDirectory(chapterPath, chapter);
            }
        }

        public void CreateChapterFilesDirectory(string chapterPath, Chapter chapter)
        {
            var filename = $"{chapter.Name}.html";
            var htmlContent = _htmlParser.CreateChapterHtmlPage(chapter);

            UtilityHelper.CreateUpdateFile(chapterPath, filename, htmlContent);

            foreach (var section in chapter.Sections)
            {
                var sectionPath = $"{chapterPath}/{chapter.Name}";

                CreateSectionFilesDirectory(sectionPath, section);
            }
        }

        public void CreateSectionFilesDirectory(string sectionPath, Section section)
        {
            var filename = $"{UtilityHelper.CreateFriendlyName(section.Name)}.html";
            var htmlContent = _htmlParser.CreateSectionHtmlPage(section);

            UtilityHelper.CreateUpdateFile(sectionPath, filename, htmlContent);
        }
    }
}
