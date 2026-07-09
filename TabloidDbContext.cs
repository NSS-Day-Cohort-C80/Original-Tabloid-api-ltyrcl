using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tabloid.Models;
using Microsoft.AspNetCore.Identity;

namespace Tabloid.Data;
public class TabloidDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<Emoji> Emojis { get; set; }
    public DbSet<Reaction> Reactions { get; set; }


    public TabloidDbContext(DbContextOptions<TabloidDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            Name = "Admin",
            NormalizedName = "admin"
        });

        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser[]
        {
            new IdentityUser
            {
                Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                UserName = "Administrator",
                Email = "admina@strator.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df",
                UserName = "JohnDoe",
                Email = "john@doe.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "a7d21fac-3b21-454a-a747-075f072d0cf3",
                UserName = "JaneSmith",
                Email = "jane@smith.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "c806cfae-bda9-47c5-8473-dd52fd056a9b",
                UserName = "AliceJohnson",
                Email = "alice@johnson.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2",
                UserName = "BobWilliams",
                Email = "bob@williams.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "d224a03d-bf0c-4a05-b728-e3521e45d74d",
                UserName = "EveDavis",
                Email = "Eve@Davis.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },

        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string>
            {
                RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
            },
            new IdentityUserRole<string>
            {
                RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                UserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df"
            },

        });
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile[]
        {
            new UserProfile
            {
                Id = 1,
                IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                FirstName = "Admina",
                LastName = "Strator",
                ImageLocation = "https://robohash.org/numquamutut.png?size=150x150&set=set1",
                CreateDateTime = new DateTime(2022, 1, 25)
            },
             new UserProfile
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                CreateDateTime = new DateTime(2023, 2, 2),
                ImageLocation = "https://robohash.org/nisiautemet.png?size=150x150&set=set1",
                IdentityUserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df",
            },
            new UserProfile
            {
                Id = 3,
                FirstName = "Jane",
                LastName = "Smith",
                CreateDateTime = new DateTime(2022, 3, 15),
                ImageLocation = "https://robohash.org/molestiaemagnamet.png?size=150x150&set=set1",
                IdentityUserId = "a7d21fac-3b21-454a-a747-075f072d0cf3",
            },
            new UserProfile
            {
                Id = 4,
                FirstName = "Alice",
                LastName = "Johnson",
                CreateDateTime = new DateTime(2023, 6, 10),
                ImageLocation = "https://robohash.org/deseruntutipsum.png?size=150x150&set=set1",
                IdentityUserId = "c806cfae-bda9-47c5-8473-dd52fd056a9b",
            },
            new UserProfile
            {
                Id = 5,
                FirstName = "Bob",
                LastName = "Williams",
                CreateDateTime = new DateTime(2023, 5, 15),
                ImageLocation = "https://robohash.org/quiundedignissimos.png?size=150x150&set=set1",
                IdentityUserId = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2",
            },
            new UserProfile
            {
                Id = 6,
                FirstName = "Eve",
                LastName = "Davis",
                CreateDateTime = new DateTime(2022, 10, 18),
                ImageLocation = "https://robohash.org/hicnihilipsa.png?size=150x150&set=set1",
                IdentityUserId = "d224a03d-bf0c-4a05-b728-e3521e45d74d",
            }
        });

        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 1, Name = "Marine Affairs" },
            new Category { Id = 2, Name = "Pirate Bounties" },
            new Category { Id = 3, Name = "Grand Line Exploration" },
            new Category { Id = 4, Name = "Devil Fruits" },
            new Category { Id = 5, Name = "World Government" }
        });

        modelBuilder.Entity<Tag>().HasData(new Tag[]
        {
            new Tag { Id = 1, Name = "Straw Hats" },
            new Tag { Id = 2, Name = "Yonko" },
            new Tag { Id = 3, Name = "Marines" },
            new Tag { Id = 4, Name = "Devil Fruit" },
            new Tag { Id = 5, Name = "Wano" },
            new Tag { Id = 6, Name = "Revolutionary Army" },
            new Tag { Id = 7, Name = "Poneglyph" }
        });

        modelBuilder.Entity<Post>().HasData(new Post[]
        {
            new Post
            {
                Id = 1,
                Title = "Monkey D. Luffy Declared the Fifth Emperor of the Sea",
                Image = "https://picsum.photos/seed/luffy/600/400",
                Body = "Following the fall of Kaido and Big Mom, the World Government has officially recognized Monkey D. Luffy as one of the Four Emperors ruling the New World. His bounty has skyrocketed to three billion berries, and Marines across the seas are on high alert. Analysts say the balance of power has shifted more in a single day than in the last decade.",
                PubDate = new DateTime(2023, 8, 1),
                UserId = 2,
                CategoryId = 2,
                Approved = true
            },
            new Post
            {
                Id = 2,
                Title = "Wano Country Finally Liberated After the Raid on Onigashima",
                Image = "https://picsum.photos/seed/wano/600/400",
                Body = "The isolated nation of Wano has thrown open its borders for the first time in generations after the Beasts Pirates were overthrown at Onigashima. Kozuki Momonosuke has been named the new shogun, vowing to honor his father's dream of one day opening the country to the world. Citizens filled the streets in celebration as the tyranny of Orochi and Kaido came to an end.",
                PubDate = new DateTime(2023, 7, 15),
                UserId = 3,
                CategoryId = 3,
                Approved = true
            },
            new Post
            {
                Id = 3,
                Title = "The Gum-Gum Fruit Was a Mythical Zoan All Along",
                Image = "https://picsum.photos/seed/nika/600/400",
                Body = "In a revelation that has stunned scholars, the fruit long recorded as the Gum-Gum Fruit is now believed to be the Hito Hito no Mi, Model: Nika, an extremely rare Mythical Zoan tied to the legend of the Sun God. The World Government reportedly hid the fruit's true nature for centuries. Researchers are scrambling to understand how a power dismissed as a simple Paramecia could hold such mythical significance.",
                PubDate = new DateTime(2023, 9, 10),
                UserId = 4,
                CategoryId = 4,
                Approved = true
            },
            new Post
            {
                Id = 4,
                Title = "Fleet Admiral Akainu Tightens His Grip on the New World",
                Image = "https://picsum.photos/seed/marines/600/400",
                Body = "Fleet Admiral Sakazuki, known as Akainu, has issued sweeping new directives to Marine forces stationed throughout the New World. Citing the rise of a new generation of pirates, he has authorized expanded patrols and harsher enforcement of Absolute Justice. Critics within the World Government warn that his uncompromising stance may push neutral islands toward rebellion.",
                PubDate = new DateTime(2023, 6, 20),
                UserId = 5,
                CategoryId = 1,
                Approved = true
            },
            new Post
            {
                Id = 5,
                Title = "Revolutionary Army Makes Its Move at the Reverie",
                Image = "https://picsum.photos/seed/revolution/600/400",
                Body = "Unconfirmed reports place members of the Revolutionary Army, including Chief of Staff Sabo, at the site of the Reverie where world leaders had gathered. Witnesses describe chaos in the holy land of Mary Geoise, though the World Government has refused to comment. If the accounts are accurate, it marks the boldest direct challenge to the Celestial Dragons in living memory.",
                PubDate = new DateTime(2023, 5, 30),
                UserId = 6,
                CategoryId = 5,
                Approved = false
            },
            new Post
            {
                Id = 6,
                Title = "Straw Hat Crew Rumored to Be Chasing the Final Road Poneglyph",
                Image = "https://picsum.photos/seed/laughtale/600/400",
                Body = "Sources across the New World claim the Straw Hat Pirates have gathered rubbings of all four Road Poneglyphs, the ancient stones said to point the way to the final island, Laugh Tale. If true, Luffy's crew would be closer to the legendary treasure known as the One Piece than any pirate since Gol D. Roger. Rival crews and Marines alike are said to be racing to intercept them.",
                PubDate = new DateTime(2023, 9, 25),
                UserId = 2,
                CategoryId = 3,
                Approved = false
            }
        });

        modelBuilder.Entity<Comment>().HasData(new Comment[]
        {
            new Comment { Id = 1, PostId = 1, UserId = 3, Subject = "Finally!", Content = "About time Luffy got the recognition he deserves." },
            new Comment { Id = 2, PostId = 1, UserId = 4, Subject = "Bounty check", Content = "3 billion berries?! That is absolutely insane." },
            new Comment { Id = 3, PostId = 2, UserId = 5, Subject = "Wano is free", Content = "Momonosuke will make a fine shogun for the land of Wano." },
            new Comment { Id = 4, PostId = 2, UserId = 6, Subject = "Kaido down", Content = "Never thought I would see the day a Yonko finally fell." },
            new Comment { Id = 5, PostId = 3, UserId = 2, Subject = "Mind blown", Content = "Hito Hito no Mi, Model: Nika. Nobody saw that coming." },
            new Comment { Id = 6, PostId = 4, UserId = 3, Subject = "Absolute Justice", Content = "Akainu's version of justice is genuinely terrifying." },
            new Comment { Id = 7, PostId = 5, UserId = 4, Subject = "Did Sabo do it?", Content = "The reports about Sabo at the Reverie are hard to believe." },
            new Comment { Id = 8, PostId = 6, UserId = 5, Subject = "Laugh Tale", Content = "If they reach it, the One Piece is real after all!" }
        });

        modelBuilder.Entity<PostTag>().HasData(new PostTag[]
        {
            new PostTag { Id = 1, PostId = 1, TagId = 1 },
            new PostTag { Id = 2, PostId = 1, TagId = 2 },
            new PostTag { Id = 3, PostId = 2, TagId = 5 },
            new PostTag { Id = 4, PostId = 2, TagId = 2 },
            new PostTag { Id = 5, PostId = 3, TagId = 4 },
            new PostTag { Id = 6, PostId = 3, TagId = 1 },
            new PostTag { Id = 7, PostId = 4, TagId = 3 },
            new PostTag { Id = 8, PostId = 5, TagId = 6 },
            new PostTag { Id = 9, PostId = 6, TagId = 1 },
            new PostTag { Id = 10, PostId = 6, TagId = 7 }
        });

        modelBuilder.Entity<Emoji>().HasData(new Emoji[]
        {
            new Emoji { Id = 1, Symbol = "🔥" },
            new Emoji { Id = 2, Symbol = "🎉" },
            new Emoji { Id = 3, Symbol = "🤯" },
            new Emoji { Id = 4, Symbol = "❤️" },
            new Emoji { Id = 5, Symbol = "🏴‍☠️" }
        });

        modelBuilder.Entity<Reaction>().HasData(new Reaction[]
        {
            new Reaction { Id = 1, EmojiId = 1, UserId = 3, PostId = 1 },
            new Reaction { Id = 2, EmojiId = 2, UserId = 4, PostId = 1 },
            new Reaction { Id = 3, EmojiId = 3, UserId = 5, PostId = 2 },
            new Reaction { Id = 4, EmojiId = 2, UserId = 2, PostId = 2 },
            new Reaction { Id = 5, EmojiId = 3, UserId = 6, PostId = 3 },
            new Reaction { Id = 6, EmojiId = 4, UserId = 2, PostId = 3 },
            new Reaction { Id = 7, EmojiId = 3, UserId = 3, PostId = 4 },
            new Reaction { Id = 8, EmojiId = 5, UserId = 4, PostId = 6 }
        });
    }
}