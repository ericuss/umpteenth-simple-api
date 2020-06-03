// Copyright (c) simple. All rights reserved.

namespace Simple.Data.Contexts
{
    using Simple.Data.Contexts.Core;

    public class LibraryContextInitializer : ContextInitializer<LibraryContext>
    {
        public static LibraryContextInitializer Instance => new LibraryContextInitializer();
    }
}
