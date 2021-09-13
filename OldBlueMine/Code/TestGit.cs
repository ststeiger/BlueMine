
// using System;

using NGit;
using NGit.Api;
// using NGit.Transport;
using NGit.Revwalk;

using NGit.Treewalk;
using NGit.Treewalk.Filter;

using NGit.Diff;

// https://github.com/ststeiger/GitRevisionControl/blob/master/TestGit/Test.cs
// namespace BlueMine.Code
namespace TestGit
{



    // http://www.mcnearney.net/blog/ngit-tutorial/
    // http://www.codeaffine.com/2014/09/22/access-git-repository-with-jgit/
    // public class TestGit
    class Test
    {


        public static string ReadFile(ObjectLoader loader)
        {
            string strModifiedFile = null;

            using (System.IO.Stream strm = loader.OpenStream())
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(strm))
                {
                    strModifiedFile = sr.ReadToEnd();
                }
            }

            return strModifiedFile;
        } // End Function ReadFile 


        public static string GetDiff(Repository repo, DiffEntry entry)
        {
            string strDiff = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                DiffFormatter diffFormatter = new DiffFormatter(ms);
                diffFormatter.SetRepository(repo);
                diffFormatter.Format(diffFormatter.ToFileHeader(entry));

                ms.Position = 0;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ms))
                {
                    strDiff = sr.ReadToEnd();
                }
            }

            return strDiff;
        } // End Function GetDiff


        // https://stackoverflow.com/questions/13537734/how-to-use-jgit-to-get-list-of-changed-files
        // https://github.com/centic9/jgit-cookbook/blob/master/src/main/java/org/dstadler/jgit/porcelain/ShowChangedFilesBetweenCommits.java
        public static void GetChanges(Git git, Repository repo, RevCommit oldCommit, RevCommit newCommit)
        {
            System.Console.WriteLine("Printing diff between commit: " + oldCommit.ToString() + " and " + newCommit.ToString());
            ObjectReader reader = repo.NewObjectReader();

            // prepare the two iterators to compute the diff between
            CanonicalTreeParser oldTreeIter = new CanonicalTreeParser();
            oldTreeIter.Reset(reader, oldCommit.Tree.Id);
            CanonicalTreeParser newTreeIter = new CanonicalTreeParser();
            newTreeIter.Reset(reader, newCommit.Tree.Id);

            // DiffStatFormatter df = new DiffStatFormatter(newCommit.Name, repo);
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {

                DiffFormatter diffFormatter = new DiffFormatter(ms);
                diffFormatter.SetRepository(repo);

                int entryCount = 0;
                foreach (DiffEntry entry in diffFormatter.Scan(oldCommit, newCommit))
                {
                    string pathToUse = null;

                    TreeWalk treeWalk = new TreeWalk(repo);
                    treeWalk.Recursive = true;

                    if (entry.GetChangeType() == DiffEntry.ChangeType.DELETE)
                    {
                        treeWalk.AddTree(oldCommit.Tree);
                        pathToUse = entry.GetOldPath();
                    }
                    else
                    {
                        treeWalk.AddTree(newCommit.Tree);
                        pathToUse = entry.GetNewPath();   
                    }

                    treeWalk.Filter = PathFilter.Create(pathToUse); 
                        
                    if (!treeWalk.Next())
                    {
                        throw new System.Exception("Did not find expected file '" + pathToUse + "'");
                    }

                    ObjectId objectId = treeWalk.GetObjectId(0);
                    ObjectLoader loader = repo.Open(objectId);

                    string strModifiedFile = ReadFile(loader);
                    System.Console.WriteLine(strModifiedFile);

                    //////////////
                    // https://stackoverflow.com/questions/27361538/how-to-show-changes-between-commits-with-jgit
                    diffFormatter.Format(diffFormatter.ToFileHeader(entry));

                    string diff = GetDiff(repo, entry);
                    System.Console.WriteLine(diff);

                    entryCount++;
                } // Next entry 

                System.Console.WriteLine(entryCount);

                ms.Position = 0;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ms))
                {
                    string strAllDiffs = sr.ReadToEnd();
                    System.Console.WriteLine(strAllDiffs);
                } // End Using sr 
                
            } // End Using ms 


            System.Collections.Generic.IList<DiffEntry> diffs = git.Diff()
                             .SetNewTree(newTreeIter)
                             .SetOldTree(oldTreeIter)
                             .Call();

            foreach (DiffEntry entry in diffs)
            {
                System.Console.WriteLine("Entry: " + entry);
                System.Console.WriteLine("Entry: " + entry.GetChangeType());
            } // Next entry 

            System.Console.WriteLine("Done");
        } // End Sub GetChanges 


        public static string GetVisualStudioPath()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            if (System.IO.Directory.Exists(path))
            {
                string[] dirz = System.IO.Directory.GetDirectories(path, "Visual Studio *", System.IO.SearchOption.TopDirectoryOnly);
                if (dirz != null)
                {
                    System.Array.Sort(dirz);
                    path = System.IO.Path.Combine(dirz[dirz.Length - 1], "Projects");
                    if (System.IO.Directory.Exists(path))
                        return path;
                } // End if (dirz != null) 

            } // End if (System.IO.Directory.Exists(path)) 

            if (System.Environment.OSVersion.Platform != System.PlatformID.Unix)
                throw new System.Exception("Visual Studio folder not found");

            return "/root/sources/";
        } // End Function GetVisualStudioPath 


        public static string GetRepoPath()
        {
            string VisualStudioPath = GetVisualStudioPath();
            // if (string.Equals(System.Environment.UserDomainName, "COR", System.StringComparison.InvariantCultureIgnoreCase))
            return System.IO.Path.Combine(VisualStudioPath, "GitRevisionControl");
        } // End Function GetRepoPath 


        // https://github.com/mono/ngit/commits/master
        public static void GetCommitsByBranch(string branchName)
        {
            // D:\Stefan.Steiger\Documents\Visual Studio 2013\Projects
            string dir = GetRepoPath();
            System.Console.WriteLine(dir);
            // dir = "https://github.com/mono/ngit.git";


            // https://github.com/centic9/jgit-cookbook/blob/master/src/main/java/org/dstadler/jgit/porcelain/ListRemoteRepository.java
            // https://stackoverflow.com/questions/13667988/how-to-use-ls-remote-in-ngit
            // git.LsRemote();


            Git git = Git.Open(dir);
            Repository repo = git.GetRepository();
            
            ObjectId branchOid = repo.Resolve(branchName);
            
            System.Console.WriteLine("Commits of branch: '{0}' ({1})", branchName, branchOid);
            System.Console.WriteLine("-------------------------------------");


            Sharpen.Iterable<RevCommit> commits = git.Log().Add(branchOid).Call();

            int count = 0;

            RevCommit laterCommit = null;

            // Note: Apparently sorted DESCENDING by COMMIT DATE
            foreach (RevCommit earlierCommit in commits)
            {
                System.Console.WriteLine(earlierCommit.Name);
                System.Console.WriteLine(earlierCommit.GetAuthorIdent().GetName());

                System.DateTime dt = UnixTimeStampToDateTime(earlierCommit.CommitTime);
                System.Console.WriteLine(dt);

                System.Console.WriteLine(earlierCommit.GetFullMessage());

                if (laterCommit != null)
                {
                    GetChanges(git, repo, earlierCommit, laterCommit);
                } // End if (laterCommit != null)

                // https://github.com/gitblit/gitblit/blob/master/src/main/java/com/gitblit/utils/JGitUtils.java#L718
                laterCommit = earlierCommit;
                count++;
            } // Next earlierCommit 

            System.Console.WriteLine(count);


            // Handle disposing of NGit's locks
            repo.Close();
            repo.ObjectDatabase.Close();
            repo = null;
            git = null;

            // https://github.com/mono/ngit/blob/master/NGit/NGit.Revwalk/RevWalkUtils.cs
        } // End Sub GetCommitsByBranch 


        public static void ListAllBranches()
        {
            // D:\Stefan.Steiger\Documents\Visual Studio 2013\Projects
            string dir = GetRepoPath();
            System.Console.WriteLine(dir);
            // dir = "https://github.com/mono/ngit.git";


            // https://github.com/centic9/jgit-cookbook/blob/master/src/main/java/org/dstadler/jgit/porcelain/ListRemoteRepository.java
            // https://stackoverflow.com/questions/13667988/how-to-use-ls-remote-in-ngit
            // git.LsRemote();


            Git git = Git.Open(dir);
            // Repository repo = git.GetRepository();


            // Get All Branches
            // https://github.com/centic9/jgit-cookbook/blob/master/src/main/java/org/dstadler/jgit/porcelain/ListBranches.java
            System.Collections.Generic.IList<Ref> branchList =
                git.BranchList().SetListMode(ListBranchCommand.ListMode.ALL).Call();

            foreach (Ref branch in branchList)
            {
                string branchName = branch.GetName();

                // if (!string.Equals(branchName, Constants.R_HEADS + "thisBranchName", System.StringComparison.InvariantCultureIgnoreCase))
                //     continue;

                if (!branchName.StartsWith(Constants.R_HEADS, System.StringComparison.InvariantCultureIgnoreCase))
                    continue;

                branchName = branchName.Substring(Constants.R_HEADS.Length);
                System.Console.WriteLine(branchName);
                // https://stackoverflow.com/questions/15822544/jgit-how-to-get-all-commits-of-a-branch-without-changes-to-the-working-direct
            } // Next branch 

            git = null;
        } // End Sub ListAllBranches 


        // https://stackoverflow.com/questions/15822544/jgit-how-to-get-all-commits-of-a-branch-without-changes-to-the-working-direct
        public static void WalkCommits()
        {
            string dir = GetRepoPath();
            Git git = Git.Open(dir);
            Repository repo = git.GetRepository();

            RevWalk walk = new RevWalk(repo);


            System.Collections.Generic.IList<Ref> branches = git.BranchList().Call();

            // https://stackoverflow.com/questions/15822544/jgit-how-to-get-all-commits-of-a-branch-without-changes-to-the-working-direct
            foreach (Ref branch in branches)
            {
                string branchName = branch.GetName();
                System.Console.WriteLine("Commits of branch: " + branchName);
                System.Console.WriteLine("-------------------------------------");

                Sharpen.Iterable<RevCommit> commits = git.Log().All().Call();

                foreach (RevCommit commit in commits)
                {
                    bool foundInThisBranch = false;
                    RevCommit targetCommit = walk.ParseCommit(repo.Resolve(commit.Name));

                    foreach (System.Collections.Generic.KeyValuePair<string, Ref> e in repo.GetAllRefs())
                    {

                        if (e.Key.StartsWith(Constants.R_HEADS))
                        {

                            if (walk.IsMergedInto(targetCommit, walk.ParseCommit(e.Value.GetObjectId())))
                            {
                                string foundInBranch = e.Value.GetName();

                                if (branchName.Equals(foundInBranch))
                                {
                                    foundInThisBranch = true;
                                    break;
                                } // End if (branchName.Equals(foundInBranch)) 

                            } // End if (walk.IsMergedInto(targetCommit, walk.ParseCommit(e.Value.GetObjectId())))

                        } // End if (e.Key.StartsWith(Constants.R_HEADS)) 

                    } // Next e

                    if (foundInThisBranch)
                    {
                        System.Console.WriteLine(commit.Name);
                        System.Console.WriteLine(commit.GetAuthorIdent().GetName());

                        // System.DateTime dt = new System.DateTime(commit.CommitTime);
                        System.DateTime dt = UnixTimeStampToDateTime(commit.CommitTime);

                        System.Console.WriteLine(dt);
                        System.Console.WriteLine(commit.GetFullMessage());
                    } // End if (foundInThisBranch) 

                } // Next commit 

            } // Next branch 

            // Handle disposing of NGit's locks
            repo.Close();
            repo.ObjectDatabase.Close();
            repo = null;
            git = null;
        } // End Sub


        // Unix timestamp is seconds past epoch
        public static System.DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


        public static void TestClone()
        {
            // git clone https://github.com/mono/ngit.git 

            // Let's clone the NGit repository
            CloneCommand clone = Git.CloneRepository()
                .SetDirectory(@"C:\Git\NGit")
                .SetURI("https://github.com/mono/ngit.git")
            ;

            // Execute and return the repository object we'll use for further commands
            Git repository = clone.Call();
        }


        public static void OpenRepo()
        {
            Git repository = Git.Open(@"C:\Git\NGit");

            // Fetch changes without merging them
            NGit.Transport.FetchResult fetch = repository.Fetch().Call();

            // Pull changes (will automatically merge/commit them)
            PullResult pull = repository.Pull().Call();

            // Get the current branch status
            Status status = repository.Status().Call();

            // The IsClean() method is helpful to check if any changes
            // have been detected in the working copy. I recommend using it,
            // as NGit will happily make a commit with no actual file changes.
            bool isClean = status.IsClean();

            // You can also access other collections related to the status
            System.Collections.Generic.ICollection<string> added = status.GetAdded();
            System.Collections.Generic.ICollection<string> changed = status.GetChanged();
            System.Collections.Generic.ICollection<string> removed = status.GetRemoved();

            // Clean our working copy
            System.Collections.Generic.ICollection<string> clean = repository.Clean().Call();

            // Add all files to the stage (you could also be more specific)
            NGit.Dircache.DirCache add = repository.Add().AddFilepattern(".").Call();

            // Remove files from the stage
            NGit.Dircache.DirCache remove = repository.Rm().AddFilepattern(".gitignore").Call();
        }


        public static void Reset()
        {
            Git repository = Git.Open(@"C:\Git\NGit");

            // git reset --hard origin/master
            Ref reset = repository.Reset()
                                .SetMode(ResetCommand.ResetType.HARD)
                                .SetRef("origin/master")
                                .Call()
            ;
        }


        public static void Commit()
        {
            Git repository = Git.Open(@"C:\Git\NGit");
            PersonIdent author = new PersonIdent("Lance Mcnearney", "lance@mcnearney.net");
            string message = "My commit message";

            // Commit our changes after adding files to the stage
            RevCommit commit = repository.Commit()
                .SetMessage(message)
                .SetAuthor(author)
                .SetAll(true) // This automatically stages modified and deleted files
                .Call();

            // Our new commit's hash
            ObjectId hash = commit.Id;

            // Push our changes back to the origin
            Sharpen.Iterable<NGit.Transport.PushResult> push = repository.Push().Call();


            // Handle disposing of NGit's locks
            repository.GetRepository().Close();
            repository.GetRepository().ObjectDatabase.Close();
            repository = null;
        }


        public static void RemoveWriteProtection()
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"C:\Git\NGit");

            System.IO.FileInfo[] files = di.GetFiles("*", System.IO.SearchOption.AllDirectories);

            // Remove the read-only attribute applied by NGit to some of its files
            foreach (System.IO.FileInfo file in files)
            {
                file.Attributes = System.IO.FileAttributes.Normal;
            }
        }


        public static void WithCreds()
        {
            Git repository = Git.Open(@"C:\Git\NGit");
            
            NGit.Transport.UsernamePasswordCredentialsProvider credentials = new NGit.Transport.UsernamePasswordCredentialsProvider("username", "password");

            // On a per-command basis
            NGit.Transport.FetchResult fetch = repository.Fetch()
                .SetCredentialsProvider(credentials)
                .Call();

            // Or globally as the default for each new command
            NGit.Transport.CredentialsProvider.SetDefault(credentials);
        }


        public static void InitRepo(string[] args)
        {
            Git myrepo = Git.Init().SetDirectory(@"/tmp/myrepo.git").SetBare(true).Call();
            {
                NGit.Transport.FetchResult fetchResult = myrepo.Fetch()
                    .SetProgressMonitor(new TextProgressMonitor())
                    .SetRemote(@"/tmp/initial")
                    .SetRefSpecs(new NGit.Transport.RefSpec("refs/heads/master:refs/heads/master"))
                    .Call();
                //
                // Some other work...
                //
                myrepo.GetRepository().Close();
            }
            System.GC.Collect();

#if false
            System.Console.WriteLine("Killing");
            BatchingProgressMonitor.ShutdownNow();
#endif
            System.Console.WriteLine("Done");

        }


    } // End Class Test 


} // End Namespace TestGit 